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
    public class ClassGroupService : IClassGroupService
    {
        private readonly IClassGroupRepository _classGroupRepository;

        public ClassGroupService(IClassGroupRepository classGroupRepository)
        {
            _classGroupRepository = classGroupRepository;
        }

        public async Task<ServiceResponse<PagedResultDto<ClassGroupDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive)
        {
            var repoResponse = await _classGroupRepository.GetAllAsync(pageNumber, pageSize, isActive);

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<PagedResultDto<ClassGroupDto>>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error al consultar los grupos."
                };
            }

            var (items, totalRecords) = repoResponse.Data;

            return new ServiceResponse<PagedResultDto<ClassGroupDto>>
            {
                Data = new PagedResultDto<ClassGroupDto>
                {
                    Items = items.Select(MapDto).ToList(),
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Grupos obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<ClassGroupDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _classGroupRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<ClassGroupDto>> CreateAsync(CreateClassGroupDto request)
        {
            var group = new ClassGroup { Name = request.Name, GradeLevel = request.GradeLevel, Description = request.Description };
            var repoResponse = await _classGroupRepository.CreateAsync(group);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<ClassGroupDto>> UpdateAsync(int id, UpdateClassGroupDto request)
        {
            var group = new ClassGroup { Name = request.Name, GradeLevel = request.GradeLevel, Description = request.Description };
            var repoResponse = await _classGroupRepository.UpdateAsync(id, group);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<ClassGroupDto>> SetActiveAsync(int id, bool isActive)
        {
            var repoResponse = await _classGroupRepository.SetActiveAsync(id, isActive);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, isActive ? "activado" : "desactivado");
        }

        private static ClassGroupDto MapDto(ClassGroup g) => new()
        {
            Id = g.Id,
            Name = g.Name,
            GradeLevel = g.GradeLevel,
            Description = g.Description,
            IsActive = g.IsActive,
            StudentCount = g.StudentCount
        };

        private static ServiceResponse<ClassGroupDto> MapResponse(int statusCode, ClassGroup? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<ClassGroupDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Grupo {action} correctamente."
                    };

                case 50160:
                    return new ServiceResponse<ClassGroupDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el grupo indicado." };

                default:
                    return new ServiceResponse<ClassGroupDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}
