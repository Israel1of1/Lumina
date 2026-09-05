using Business;
using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Mvc;


namespace Backend.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            return MapResponse(result);
        }

        [HttpGet("link-code/{code}")]
        public async Task<IActionResult> ValidateLinkCode(string code)
        {
            var result = await _authService.ValidateLinkCodeAsync(code);
            return MapResponse(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            var result = await _authService.RegisterWithLinkCodeAsync(request);
            return MapResponse(result);
        }

        private IActionResult MapResponse<T>(ServiceResponse<T> result)
        {
            if (result.IsSuccess)
                return Ok(result);

            return result.MessageCodes switch
            {
                MessageCodes.ErrorValidation => BadRequest(result),
                MessageCodes.Unauthorized => Unauthorized(result),
                MessageCodes.NotFound => NotFound(result),
                MessageCodes.Conflict => Conflict(result),
                _ => StatusCode(500, result)
            };
        }
    }
}