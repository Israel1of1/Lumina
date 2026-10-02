using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class HabitComplianceService : IHabitComplianceService
    {
        private readonly IHabitComplianceRepository _habitComplianceRepository;

        public HabitComplianceService(IHabitComplianceRepository habitComplianceRepository)
        {
            _habitComplianceRepository = habitComplianceRepository;
        }

        public async Task<ServiceResponse<List<HabitComplianceDto>>> GetByHabitAsync(int habitId)
        {
            var repoResponse = await _habitComplianceRepository.GetByHabitAsync(habitId);

            if (repoResponse.OperationStatusCode == 50220)
            {
                return new ServiceResponse<List<HabitComplianceDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el habito indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<List<HabitComplianceDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar los cumplimientos." };
            }

            return new ServiceResponse<List<HabitComplianceDto>>
            {
                Data = repoResponse.Data!.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Cumplimientos obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<HabitComplianceDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _habitComplianceRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<HabitComplianceDto>> CreateAsync(CreateHabitComplianceDto request)
        {
            var compliance = new HabitCompliance
            {
                HabitId = request.HabitId,
                ComplianceDate = request.ComplianceDate,
                IsFulfilled = request.IsFulfilled,
                Observation = request.Observation,
                RegisteredById = request.RegisteredById
            };

            var repoResponse = await _habitComplianceRepository.CreateAsync(compliance);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<HabitComplianceDto>> UpdateAsync(int id, UpdateHabitComplianceDto request)
        {
            var compliance = new HabitCompliance
            {
                HabitId = request.HabitId,
                ComplianceDate = request.ComplianceDate,
                IsFulfilled = request.IsFulfilled,
                Observation = request.Observation,
                RegisteredById = request.RegisteredById
            };

            var repoResponse = await _habitComplianceRepository.UpdateAsync(id, compliance);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var repoResponse = await _habitComplianceRepository.DeleteAsync(id);

            if (repoResponse.OperationStatusCode == 50222)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el registro de cumplimiento indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al eliminar el registro." };
            }

            return new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Registro de cumplimiento eliminado correctamente." };
        }

        private static HabitComplianceDto MapDto(HabitCompliance hc) => new()
        {
            Id = hc.Id,
            HabitId = hc.HabitId,
            ComplianceDate = hc.ComplianceDate,
            IsFulfilled = hc.IsFulfilled,
            Observation = hc.Observation,
            RegisteredById = hc.RegisteredById,
            CreatedAt = hc.CreatedAt
        };

        private static ServiceResponse<HabitComplianceDto> MapResponse(int statusCode, HabitCompliance? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<HabitComplianceDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Registro de cumplimiento {action} correctamente."
                    };

                case 50220:
                    return new ServiceResponse<HabitComplianceDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el habito indicado." };

                case 50221:
                    return new ServiceResponse<HabitComplianceDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el usuario que registra." };

                case 50222:
                    return new ServiceResponse<HabitComplianceDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el registro de cumplimiento indicado." };

                default:
                    return new ServiceResponse<HabitComplianceDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}