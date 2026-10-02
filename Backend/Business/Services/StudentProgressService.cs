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
    public class StudentProgressService : IStudentProgressService
    {
        private readonly IStudentProgressRepository _studentProgressRepository;

        public StudentProgressService(IStudentProgressRepository studentProgressRepository)
        {
            _studentProgressRepository = studentProgressRepository;
        }

        public async Task<ServiceResponse<List<StudentProgressDto>>> GetByStudentAsync(int studentId)
        {
            var repoResponse = await _studentProgressRepository.GetByStudentAsync(studentId);

            if (repoResponse.OperationStatusCode == 50230)
            {
                return new ServiceResponse<List<StudentProgressDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<List<StudentProgressDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar el progreso." };
            }

            return new ServiceResponse<List<StudentProgressDto>>
            {
                Data = repoResponse.Data!.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Progreso obtenido correctamente."
            };
        }

        public async Task<ServiceResponse<StudentProgressDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _studentProgressRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<StudentProgressDto>> CreateAsync(CreateStudentProgressDto request)
        {
            var progress = new StudentProgress
            {
                StudentId = request.StudentId,
                CompletionPercentage = request.CompletionPercentage,
                CurrentLevel = request.CurrentLevel,
                Strengths = request.Strengths,
                Weaknesses = request.Weaknesses,
                Recommendation = request.Recommendation,
                TotalStudyTime = request.TotalStudyTime,
                LastSessionAt = request.LastSessionAt
            };

            var repoResponse = await _studentProgressRepository.CreateAsync(progress);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<StudentProgressDto>> UpdateAsync(int id, UpdateStudentProgressDto request)
        {
            var progress = new StudentProgress
            {
                CompletionPercentage = request.CompletionPercentage,
                CurrentLevel = request.CurrentLevel,
                Strengths = request.Strengths,
                Weaknesses = request.Weaknesses,
                Recommendation = request.Recommendation,
                TotalStudyTime = request.TotalStudyTime,
                LastSessionAt = request.LastSessionAt
            };

            var repoResponse = await _studentProgressRepository.UpdateAsync(id, progress);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var repoResponse = await _studentProgressRepository.DeleteAsync(id);

            if (repoResponse.OperationStatusCode == 50231)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el registro de progreso indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al eliminar el registro." };
            }

            return new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Registro de progreso eliminado correctamente." };
        }

        private static StudentProgressDto MapDto(StudentProgress p) => new()
        {
            Id = p.Id,
            StudentId = p.StudentId,
            CompletionPercentage = p.CompletionPercentage,
            CurrentLevel = p.CurrentLevel,
            Strengths = p.Strengths,
            Weaknesses = p.Weaknesses,
            Recommendation = p.Recommendation,
            TotalStudyTime = p.TotalStudyTime,
            LastSessionAt = p.LastSessionAt,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        };

        private static ServiceResponse<StudentProgressDto> MapResponse(int statusCode, StudentProgress? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<StudentProgressDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Registro de progreso {action} correctamente."
                    };

                case 50230:
                    return new ServiceResponse<StudentProgressDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50231:
                    return new ServiceResponse<StudentProgressDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el registro de progreso indicado." };

                default:
                    return new ServiceResponse<StudentProgressDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}