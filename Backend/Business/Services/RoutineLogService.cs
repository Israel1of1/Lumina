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
    public class RoutineLogService : IRoutineLogService
    {
        private readonly IRoutineLogRepository _logRepository;
        private readonly IRoutineDetailRepository _detailRepository;
        private readonly IAccessControlRepository _accessControlRepository;

        public RoutineLogService(
            IRoutineLogRepository logRepository,
            IRoutineDetailRepository detailRepository,
            IAccessControlRepository accessControlRepository)
        {
            _logRepository = logRepository;
            _detailRepository = detailRepository;
            _accessControlRepository = accessControlRepository;
        }

        public async Task<ServiceResponse<RoutineLogDto>> CreateAsync(int requestingUserId, CreateRoutineLogDto request)
        {
            var detail = await _detailRepository.GetByIdWithStudentAsync(request.RoutineDetailId);
            if (detail.OperationStatusCode == 50201)
                return NotFound("No se encontro el paso de rutina indicado.");

            if (detail.Data!.StudentId != request.StudentId)
                return new ServiceResponse<RoutineLogDto>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorValidation,
                    Message = "El estudiante indicado no corresponde a ese paso de rutina."
                };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, request.StudentId))
                return Denied<RoutineLogDto>();

            var log = new RoutineLog
            {
                RoutineDetailId = request.RoutineDetailId,
                StudentId = request.StudentId,
                Status = request.Status,
                Observation = request.Observation,
                LogDate = request.LogDate,
                RegisteredById = requestingUserId
            };

            var repoResponse = await _logRepository.CreateAsync(log);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<List<RoutineLogDto>>> GetByStudentAsync(int requestingUserId, int studentId, DateTime? fromDate, DateTime? toDate)
        {
            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, studentId))
                return Denied<List<RoutineLogDto>>();

            var repoResponse = await _logRepository.GetByStudentAsync(studentId, fromDate, toDate);

            if (repoResponse.OperationStatusCode == 50170)
                return new ServiceResponse<List<RoutineLogDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

            return new ServiceResponse<List<RoutineLogDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Registros de cumplimiento obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<List<RoutineLogDto>>> GetByDetailAsync(int requestingUserId, int routineDetailId)
        {
            var detail = await _detailRepository.GetByIdWithStudentAsync(routineDetailId);
            if (detail.OperationStatusCode == 50201)
                return new ServiceResponse<List<RoutineLogDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el paso de rutina indicado." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, detail.Data!.StudentId))
                return Denied<List<RoutineLogDto>>();

            var repoResponse = await _logRepository.GetByDetailAsync(routineDetailId);

            return new ServiceResponse<List<RoutineLogDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Registros de cumplimiento obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<RoutineLogDto>> UpdateAsync(int requestingUserId, int id, UpdateRoutineLogDto request)
        {
            var existing = await _logRepository.GetByIdAsync(id);
            if (existing.OperationStatusCode == 50203)
                return NotFound("No se encontro el registro indicado.");

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, existing.Data!.StudentId))
                return Denied<RoutineLogDto>();

            var log = new RoutineLog { Status = request.Status, Observation = request.Observation };
            var repoResponse = await _logRepository.UpdateAsync(id, log);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        private static RoutineLogDto MapDto(RoutineLog l) => new()
        {
            Id = l.Id,
            RoutineDetailId = l.RoutineDetailId,
            StudentId = l.StudentId,
            Status = l.Status,
            Observation = l.Observation,
            LogDate = l.LogDate
        };

        private static ServiceResponse<RoutineLogDto> MapResponse(int statusCode, RoutineLog? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<RoutineLogDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Registro {action} correctamente." };
                case 50170:
                    return NotFound("No se encontro el estudiante indicado.");
                case 50201:
                    return NotFound("No se encontro el paso de rutina indicado.");
                case 50203:
                    return NotFound("No se encontro el registro indicado.");
                default:
                    return new ServiceResponse<RoutineLogDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }

        private static ServiceResponse<RoutineLogDto> NotFound(string message) => new()
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
