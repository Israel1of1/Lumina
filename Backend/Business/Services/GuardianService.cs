using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class GuardianService: IGuardianService
    {
        private readonly IGuardianRepository _guardianRepository;

        public GuardianService(IGuardianRepository guardianRepository)
        {
            _guardianRepository = guardianRepository;
        }

        public async Task<ServiceResponse<GuardianProfileDto>> GetMyProfileAsync(int userId)
        {
            var repoResponse = await _guardianRepository.GetByUserIdAsync(userId);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<GuardianProfileDto>> UpdateMyProfileAsync(int userId, UpdateGuardianProfileDto request)
        {
            var profile = new Guardian
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalId = request.NationalId,
                PersonalEmail = request.PersonalEmail,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                Photo = request.Photo,
                relationship = request.Relationship
            };

            var repoResponse = await _guardianRepository.UpdateProfileAsync(userId, profile);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        private static ServiceResponse<GuardianProfileDto> MapResponse(int statusCode, Guardian? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<GuardianProfileDto>
                    {
                        Data = new GuardianProfileDto
                        {
                            Id = data!.Id,
                            FirstName = data.FirstName,
                            LastName = data.LastName,
                            NationalId = data.NationalId,
                            PersonalEmail = data.PersonalEmail,
                            Phone = data.Phone,
                            Address = data.Address,
                            City = data.City,
                            Photo = data.Photo,
                            Relationship = data.relationship,
                            EntityStatus = data.EntityStatus
                        },
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Perfil {action} correctamente."
                    };

                case 5095:
                    return new ServiceResponse<GuardianProfileDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = "No se encontro un perfil de tutor vinculado a esta cuenta."
                    };

                case 5097:
                    return new ServiceResponse<GuardianProfileDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = "Ya existe otro tutor registrado con ese numero de identificacion."
                    };

                default:
                    return new ServiceResponse<GuardianProfileDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = "Ocurrio un error inesperado."
                    };
            }
        }

        public async Task<ServiceResponse<PagedResultDto<GuardianProfileDto>>> GetAllAsync(int pageNumber, int pageSize, string? status)
        {
            try
            {
                var repoResponse = await _guardianRepository.GetAllAsync(pageNumber, pageSize, status);

                if (repoResponse.OperationStatusCode != 0)
                {
                    return new ServiceResponse<PagedResultDto<GuardianProfileDto>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = "Ocurrio un error al consultar los tutores."
                    };
                }

                var (items, totalRecords) = repoResponse.Data;

                return new ServiceResponse<PagedResultDto<GuardianProfileDto>>
                {
                    Data = new PagedResultDto<GuardianProfileDto>
                    {
                        Items = items.Select(g => new GuardianProfileDto
                        {
                            Id = g.Id,
                            FirstName = g.FirstName,
                            LastName = g.LastName,
                            NationalId = g.NationalId,
                            PersonalEmail = g.PersonalEmail,
                            Phone = g.Phone,
                            Relationship = g.relationship,
                            EntityStatus = g.EntityStatus
                        }).ToList(),
                        TotalRecords = totalRecords,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    },
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Tutores obtenidos correctamente."
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<PagedResultDto<GuardianProfileDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio algo inesperado."
                };
            }
        }

        public async Task<ServiceResponse<EntityStatusDto>> DeactivateAsync(int guardianId, string? reason)
        {
            var repoResponse = await _guardianRepository.DeactivateAsync(guardianId, reason);

            switch (repoResponse.OperationStatusCode)
            {
                case 0:
                    return new ServiceResponse<EntityStatusDto>
                    {
                        Data = new EntityStatusDto
                        {
                            Id = repoResponse.Data!.Id,
                            FirstName = repoResponse.Data.FirstName,
                            LastName = repoResponse.Data.LastName,
                            EntityStatus = repoResponse.Data.EntityStatus,
                            DismissalDate = repoResponse.Data.DismissalDate,
                            DismissalReason = repoResponse.Data.DismissalReason
                        },
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Tutor dado de baja correctamente."
                    };

                case 5095:
                    return new ServiceResponse<EntityStatusDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = "No se encontro el tutor indicado."
                    };

                case 5098:
                    return new ServiceResponse<EntityStatusDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = "Este tutor ya se encuentra inactivo."
                    };

                default:
                    return new ServiceResponse<EntityStatusDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = "Ocurrio un error inesperado al dar de baja al tutor."
                    };
            }
        }
    }
}
