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
    public class StudentHabitService : IStudentHabitService
    {
        private readonly IStudentHabitRepository _studentHabitRepository;

        public StudentHabitService(IStudentHabitRepository studentHabitRepository)
        {
            _studentHabitRepository = studentHabitRepository;
        }

        public async Task<ServiceResponse<List<StudentHabitDto>>> GetByStudentAsync(int studentId)
        {
            var repoResponse = await _studentHabitRepository.GetByStudentAsync(studentId);

            if (repoResponse.OperationStatusCode == 50210)
            {
                return new ServiceResponse<List<StudentHabitDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<List<StudentHabitDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar los habitos." };
            }

            return new ServiceResponse<List<StudentHabitDto>>
            {
                Data = repoResponse.Data!.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Habitos obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<StudentHabitDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _studentHabitRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<StudentHabitDto>> CreateAsync(CreateStudentHabitDto request)
        {
            var habit = new StudentHabit
            {
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                Name = request.Name,
                Frequency = request.Frequency,
                Observations = request.Observations
            };

            var repoResponse = await _studentHabitRepository.CreateAsync(habit);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<StudentHabitDto>> UpdateAsync(int id, UpdateStudentHabitDto request)
        {
            var habit = new StudentHabit
            {
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                Name = request.Name,
                Frequency = request.Frequency,
                Observations = request.Observations
            };

            var repoResponse = await _studentHabitRepository.UpdateAsync(id, habit);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var repoResponse = await _studentHabitRepository.DeleteAsync(id);

            if (repoResponse.OperationStatusCode == 50212)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el habito indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al eliminar el habito." };
            }

            return new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Habito eliminado correctamente." };
        }

        private static StudentHabitDto MapDto(StudentHabit h) => new()
        {
            Id = h.Id,
            StudentId = h.StudentId,
            SubjectId = h.SubjectId,
            Name = h.Name,
            Frequency = h.Frequency,
            Observations = h.Observations,
            CreatedAt = h.CreatedAt
        };

        private static ServiceResponse<StudentHabitDto> MapResponse(int statusCode, StudentHabit? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<StudentHabitDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Habito {action} correctamente."
                    };

                case 50210:
                    return new ServiceResponse<StudentHabitDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50211:
                    return new ServiceResponse<StudentHabitDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la materia indicada." };

                case 50212:
                    return new ServiceResponse<StudentHabitDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el habito indicado." };

                default:
                    return new ServiceResponse<StudentHabitDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}