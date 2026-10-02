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

    public class RoutineService : IRoutineService
    {
        private readonly IRoutineRepository _routineRepository;
        private readonly IAccessControlRepository _accessControlRepository;

        public RoutineService(IRoutineRepository routineRepository, IAccessControlRepository accessControlRepository)
        {
            _routineRepository = routineRepository;
            _accessControlRepository = accessControlRepository;
        }

        public async Task<ServiceResponse<RoutineDto>> CreateAsync(int requestingUserId, CreateRoutineDto request)
        {
            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, request.StudentId))
                return Denied<RoutineDto>();

            var routine = new Routine
            {
                StudentId = request.StudentId,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                CreatedByUserId = requestingUserId
            };

            var repoResponse = await _routineRepository.CreateAsync(routine);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creada");
        }

        public async Task<ServiceResponse<List<RoutineDto>>> GetByStudentAsync(int requestingUserId, int studentId, string? status)
        {
            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, studentId))
                return Denied<List<RoutineDto>>();

            var repoResponse = await _routineRepository.GetByStudentAsync(studentId, status);

            if (repoResponse.OperationStatusCode == 50170)
                return new ServiceResponse<List<RoutineDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

            if (repoResponse.OperationStatusCode != 0)
                return new ServiceResponse<List<RoutineDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar las rutinas." };

            return new ServiceResponse<List<RoutineDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Rutinas obtenidas correctamente."
            };
        }

        public async Task<ServiceResponse<RoutineDto>> GetByIdAsync(int requestingUserId, int id)
        {
            var repoResponse = await _routineRepository.GetByIdAsync(id);

            if (repoResponse.OperationStatusCode == 50200)
                return new ServiceResponse<RoutineDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la rutina indicada." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, repoResponse.Data!.StudentId))
                return Denied<RoutineDto>();

            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultada");
        }

        public async Task<ServiceResponse<RoutineDto>> UpdateAsync(int requestingUserId, int id, UpdateRoutineDto request)
        {
            var existing = await _routineRepository.GetByIdAsync(id);
            if (existing.OperationStatusCode == 50200)
                return new ServiceResponse<RoutineDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la rutina indicada." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, existing.Data!.StudentId))
                return Denied<RoutineDto>();

            var routine = new Routine { Name = request.Name, Description = request.Description, StartDate = request.StartDate, EndDate = request.EndDate };
            var repoResponse = await _routineRepository.UpdateAsync(id, routine);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizada");
        }

        public async Task<ServiceResponse<RoutineDto>> SetStatusAsync(int requestingUserId, int id, string status)
        {
            var existing = await _routineRepository.GetByIdAsync(id);
            if (existing.OperationStatusCode == 50200)
                return new ServiceResponse<RoutineDto> { Data = null, IsSuccess = false, MessageCodes= MessageCodes.NotFound, Message = "No se encontro la rutina indicada." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, existing.Data!.StudentId))
                return Denied<RoutineDto>();

            var repoResponse = await _routineRepository.SetStatusAsync(id, status);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizada");
        }

        private static RoutineDto MapDto(Routine r) => new()
        {
            Id = r.Id,
            StudentId = r.StudentId,
            Name = r.Name,
            Description = r.Description,
            StartDate = r.StartDate,
            EndDate = r.EndDate,
            Status = r.Status
        };

        private static ServiceResponse<RoutineDto> MapResponse(int statusCode, Routine? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<RoutineDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Rutina {action} correctamente." };

                case 50170:
                    return new ServiceResponse<RoutineDto> { Data = null, IsSuccess = false, MessageCodes= MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50200:
                    return new ServiceResponse<RoutineDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la rutina indicada." };

                default:
                    return new ServiceResponse<RoutineDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }

        private static ServiceResponse<T> Denied<T>() => new()
        {
            Data = default,
            IsSuccess = false,
            MessageCodes = MessageCodes.Unauthorized,
            Message = "No tienes permiso para acceder a la informacion de este estudiante."
        };
    }
}
