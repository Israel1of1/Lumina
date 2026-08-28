using Business;
using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        private string GenerateTokenJWT(User user)
        {
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // Aqui SI puede haber mas de un rol (Docente y/o Tutor), por eso se
            // agrega un claim de Role por cada uno en vez de uno solo.
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(8);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                signingCredentials: credentials,
                expires: expires
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public async Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest)
        {
            try
            {
                var existentUser = await _authRepository.GetByEmailAsync(loginRequest.Email);

                if (existentUser.OperationStatusCode == 5060 || existentUser.Data == null || string.IsNullOrEmpty(existentUser.Data.Email))
                {
                    return new ServiceResponse<LoginResponseDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Unauthorized,
                        Message = "Correo o contraseña incorrectos."
                    };
                }

                if (!existentUser.Data.IsActive)
                {
                    return new ServiceResponse<LoginResponseDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Unauthorized,
                        Message = "Esta cuenta se encuentra inactiva. Contacta a la institucion."
                    };
                }

                var isValidPassword = BCrypt.Net.BCrypt.Verify(loginRequest.Password, existentUser.Data.PasswordHash);

                if (!isValidPassword)
                {
                    return new ServiceResponse<LoginResponseDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Unauthorized,
                        Message = "Correo o contraseña incorrectos."
                    };
                }

                var token = GenerateTokenJWT(existentUser.Data);

                var loginResponse = new LoginResponseDto
                {
                    UserId = existentUser.Data.Id,
                    Email = existentUser.Data.Email,
                    Roles = existentUser.Data.Roles,
                    Token = token
                };

                return new ServiceResponse<LoginResponseDto>
                {
                    Data = loginResponse,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Inicio de sesion exitoso."
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<LoginResponseDto>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio algo inesperado."
                };
            }
        }

        public async Task<ServiceResponse<LinkCodeInfoDto>> ValidateLinkCodeAsync(string code)
        {
            try
            {
                var linkCodeResult = await _authRepository.GetLinkCodeInfoAsync(code);

                if (linkCodeResult.OperationStatusCode == 5080 || linkCodeResult.Data == null)
                {
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = "El codigo de vinculacion no existe."
                    };
                }

                var isExpired = linkCodeResult.Data.ExpiresAt.HasValue && linkCodeResult.Data.ExpiresAt.Value < DateTime.UtcNow;

                if (linkCodeResult.Data.Status != "PENDING" || isExpired)
                {
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = "El codigo de vinculacion ya fue usado, expiro o fue revocado."
                    };
                }

                return new ServiceResponse<LinkCodeInfoDto>
                {
                    Data = new LinkCodeInfoDto
                    {
                        Purpose = linkCodeResult.Data.Purpose,
                        ExpiresAt = linkCodeResult.Data.ExpiresAt
                    },
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Codigo valido."
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<LinkCodeInfoDto>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio algo inesperado."
                };
            }
        }

        public async Task<ServiceResponse<LoginResponseDto>> RegisterWithLinkCodeAsync(RegisterUserDto request)
        {
            try
            {
                if (request.Password != request.ConfirmPassword)
                {
                    return new ServiceResponse<LoginResponseDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorValidation,
                        Message = "Las contraseñas no coinciden."
                    };
                }

                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                var repoResponse = await _authRepository.RegisterWithLinkCodeAsync(request.Code, request.Email, passwordHash);

                switch (repoResponse.OperationStatusCode)
                {
                    case 0:
                        var token = GenerateTokenJWT(repoResponse.Data!);

                        return new ServiceResponse<LoginResponseDto>
                        {
                            Data = new LoginResponseDto
                            {
                                UserId = repoResponse.Data!.Id,
                                Email = repoResponse.Data.Email,
                                Roles = repoResponse.Data.Roles,
                                Token = token
                            },
                            IsSuccess = true,
                            MessageCodes = MessageCodes.Success,
                            Message = "Cuenta creada correctamente."
                        };

                    case 5080:
                        return new ServiceResponse<LoginResponseDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = "El codigo de vinculacion no existe."
                        };

                    case 5081:
                        return new ServiceResponse<LoginResponseDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.Conflict,
                            Message = "El codigo de vinculacion ya fue usado, expiro o fue revocado."
                        };

                    case 5061:
                        return new ServiceResponse<LoginResponseDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.Conflict,
                            Message = "Ya existe una cuenta con ese correo."
                        };

                    case 5050:
                        return new ServiceResponse<LoginResponseDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = "El rol correspondiente no esta configurado en el sistema."
                        };

                    default:
                        return new ServiceResponse<LoginResponseDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = "Ocurrio un error inesperado al crear la cuenta."
                        };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<LoginResponseDto>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = $"Ocurrio un error inesperado: {ex.Message}"
                };
            }
        }
    }
}