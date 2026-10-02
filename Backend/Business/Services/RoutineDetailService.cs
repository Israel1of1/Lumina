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
    public class RoutineDetailService : IRoutineDetailService
    {
        private readonly IRoutineDetailRepository _detailRepository;
        private readonly IRoutineRepository _routineRepository;
        private readonly IAccessControlRepository _accessControlRepository;

        public RoutineDetailService(
            IRoutineDetailRepository detailRepository,
            IRoutineRepository routineRepository,
            IAccessControlRepository accessControlRepository)
        {
            _detailRepository = detailRepository;
            _routineRepository = routineRepository;
            _accessControlRepository = accessControlRepository;
        }

        public async Task<ServiceResponse<RoutineDetailDto>> CreateAsync(int requestingUserId, CreateRoutineDetailDto request)
        {
            var routine = await _routineRepository.GetByIdAsync(request.RoutineId);
            if (routine.OperationStatusCode == 50200)
                return NotFoundResponse("No se encontro la rutina indicada.");

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, routine.Data!.StudentId))
                return Denied<RoutineDetailDto>();

            var detail = new RoutineDetail
            {
                RoutineId = request.RoutineId,
                TimeOfDay = request.TimeOfDay,
                Activity = request.Activity,
                Description = request.Description,
                DurationMinutes = request.DurationMinutes
            };

            var repoResponse = await _detailRepository.CreateAsync(detail);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<List<RoutineDetailDto>>> GetByRoutineAsync(int requestingUserId, int routineId)
        {
            var routine = await _routineRepository.GetByIdAsync(routineId);
            if (routine.OperationStatusCode == 50200)
                return new ServiceResponse<List<RoutineDetailDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la rutina indicada." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, routine.Data!.StudentId))
                return Denied<List<RoutineDetailDto>>();

            var repoResponse = await _detailRepository.GetByRoutineAsync(routineId);

            return new ServiceResponse<List<RoutineDetailDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Pasos de la rutina obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<RoutineDetailDto>> UpdateAsync(int requestingUserId, int id, UpdateRoutineDetailDto request)
        {
            var (problem, _) = await CheckAccessByDetailId(requestingUserId, id);
            if (problem == "notfound")
                return NotFoundResponse("No se encontro el paso de rutina indicado.");
            if (problem == "denied")
                return Denied<RoutineDetailDto>();

            var detail = new RoutineDetail { TimeOfDay = request.TimeOfDay, Activity = request.Activity, Description = request.Description, DurationMinutes = request.DurationMinutes };
            var repoResponse = await _detailRepository.UpdateAsync(id, detail);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int requestingUserId, int id)
        {
            var (problem, _) = await CheckAccessByDetailId(requestingUserId, id);
            if (problem == "notfound")
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes= MessageCodes.NotFound, Message = "No se encontro el paso de rutina indicado." };
            if (problem == "denied")
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.Unauthorized, Message = "No tienes permiso para modificar esta rutina." };

            var repoResponse = await _detailRepository.DeleteAsync(id);

            switch (repoResponse.OperationStatusCode)
            {
                case 0:
                    return new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Paso de rutina eliminado correctamente." };
                case 50201:
                    return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el paso de rutina indicado." };
                case 50202:
                    return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.Conflict, Message = "No se puede eliminar: ya tiene registros de cumplimiento asociados." };
                default:
                    return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }

        /// <summary>
        /// Resuelve el estudiante al que pertenece un RoutineDetail y valida que requestingUserId
        /// tenga acceso a el. Retorna (null, studentId) si todo OK, ("notfound", 0) o ("denied", 0) si hay problema.
        /// </summary>
        private async Task<(string? Problem, int StudentId)> CheckAccessByDetailId(int requestingUserId, int detailId)
        {
            var detailResponse = await _detailRepository.GetByIdWithStudentAsync(detailId);
            if (detailResponse.OperationStatusCode == 50201)
                return ("notfound", 0);

            var hasAccess = await _accessControlRepository.HasStudentAccessAsync(requestingUserId, detailResponse.Data!.StudentId);
            return hasAccess ? (null, detailResponse.Data.StudentId) : ("denied", 0);
        }

        private static RoutineDetailDto MapDto(RoutineDetail d) => new()
        {
            Id = d.Id,
            RoutineId = d.RoutineId,
            TimeOfDay = d.TimeOfDay,
            Activity = d.Activity,
            Description = d.Description,
            DurationMinutes = d.DurationMinutes
        };

        private static ServiceResponse<RoutineDetailDto> MapResponse(int statusCode, RoutineDetail? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<RoutineDetailDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Paso de rutina {action} correctamente." };
                case 50200:
                    return NotFoundResponse("No se encontro la rutina indicada.");
                case 50201:
                    return NotFoundResponse("No se encontro el paso de rutina indicado.");
                default:
                    return new ServiceResponse<RoutineDetailDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }

        private static ServiceResponse<RoutineDetailDto> NotFoundResponse(string message) => new()
        {
            Data = null,
            IsSuccess = false,
            MessageCodes = MessageCodes.NotFound,
            Message = message
        };

        private static ServiceResponse<T> Denied<T>() => new()
        {
            Data = default,
            IsSuccess = false,
            MessageCodes = MessageCodes.Unauthorized,
            Message = "No tienes permiso para acceder a la informacion de este estudiante."
        };
    }
}
