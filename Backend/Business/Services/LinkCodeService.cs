using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class LinkCodeService : ILinkCodeService
    {
        private readonly ILinkCodeRepository _linkCodeRepository;

        public LinkCodeService(ILinkCodeRepository linkCodeRepository)
        {
            _linkCodeRepository = linkCodeRepository;
        }

        public async Task<ServiceResponse<LinkCodeInfoDto>> CreateForTeacherAsync(CreateTeacherLinkCodeDto request, int? issuedById)
        {
            var repoResponse = await _linkCodeRepository.CreateForTeacherAsync(request.TeacherId, issuedById, request.ExpiresAt);
            return MapCreateResponse(repoResponse.OperationStatusCode, repoResponse.Data, entityLabel: "docente");
        }

        public async Task<ServiceResponse<LinkCodeInfoDto>> CreateForGuardianAsync(CreateGuardianLinkCodeDto request, int? issuedById)
        {
            var repoResponse = await _linkCodeRepository.CreateForGuardianAsync(request.GuardianId, issuedById, request.ExpiresAt);
            return MapCreateResponse(repoResponse.OperationStatusCode, repoResponse.Data, entityLabel: "tutor");
        }

        public async Task<ServiceResponse<bool>> RevokeAsync(string code)
        {
            var repoResponse = await _linkCodeRepository.RevokeAsync(code);

            return repoResponse.OperationStatusCode switch
            {
                0 => new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Codigo revocado correctamente."
                },
                5080 => new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.NotFound,
                    Message = "El codigo de vinculacion no existe."
                },
                5081 => new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.Conflict,
                    Message = "El codigo ya fue usado, ya esta revocado o expiro; no se puede revocar."
                },
                _ => new ServiceResponse<bool>
                {
                    Data = false,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error inesperado al revocar el codigo."
                }
            };
        }

        private static ServiceResponse<LinkCodeInfoDto> MapCreateResponse(int statusCode, Core.Entities.LinkCodeInfo? data, string entityLabel)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = new LinkCodeInfoDto
                        {
                            Code = data!.Code,
                            Purpose = data.Purpose,
                            Status = data.Status,
                            ExpiresAt = data.ExpiresAt
                        },
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Codigo de vinculacion generado correctamente."
                    };

                case 5090:
                case 5095:
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = $"No se encontro el registro de {entityLabel} indicado."
                    };

                case 5091:
                case 5096:
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = $"Este {entityLabel} ya tiene una cuenta vinculada."
                    };

                case 5082:
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = $"Ya existe un codigo pendiente sin usar para este {entityLabel}. Revocalo antes de generar uno nuevo."
                    };

                case 5099:
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = "No se pudo generar un codigo unico, intenta de nuevo."
                    };

                default:
                    return new ServiceResponse<LinkCodeInfoDto>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = "Ocurrio un error inesperado al generar el codigo."
                    };
            }
        }
    }
}
