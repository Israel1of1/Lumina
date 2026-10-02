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
    public class StudyHistoryService : IStudyHistoryService
    {
        private readonly IStudyHistoryRepository _studyHistoryRepository;

        public StudyHistoryService(IStudyHistoryRepository studyHistoryRepository)
        {
            _studyHistoryRepository = studyHistoryRepository;
        }

        public async Task<ServiceResponse<List<StudyHistoryDto>>> GetByStudentAsync(int studentId)
        {
            var repoResponse = await _studyHistoryRepository.GetByStudentAsync(studentId);

            if (repoResponse.OperationStatusCode == 50240)
            {
                return new ServiceResponse<List<StudyHistoryDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<List<StudyHistoryDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar el historial." };
            }

            return new ServiceResponse<List<StudyHistoryDto>>
            {
                Data = repoResponse.Data!.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Historial obtenido correctamente."
            };
        }

        public async Task<ServiceResponse<StudyHistoryDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _studyHistoryRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<StudyHistoryDto>> CreateAsync(CreateStudyHistoryDto request)
        {
            var history = new StudyHistory
            {
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                LessonId = request.LessonId,
                Score = request.Score,
                StudyTime = request.StudyTime,
                StudyDate = request.StudyDate,
                Result = request.Result,
                Difficulty = request.Difficulty
            };

            var repoResponse = await _studyHistoryRepository.CreateAsync(history);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<StudyHistoryDto>> UpdateAsync(int id, UpdateStudyHistoryDto request)
        {
            var history = new StudyHistory
            {
                SubjectId = request.SubjectId,
                LessonId = request.LessonId,
                Score = request.Score,
                StudyTime = request.StudyTime,
                StudyDate = request.StudyDate,
                Result = request.Result,
                Difficulty = request.Difficulty
            };

            var repoResponse = await _studyHistoryRepository.UpdateAsync(id, history);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var repoResponse = await _studyHistoryRepository.DeleteAsync(id);

            if (repoResponse.OperationStatusCode == 50243)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el registro de historial indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al eliminar el registro." };
            }

            return new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Registro de historial eliminado correctamente." };
        }

        private static StudyHistoryDto MapDto(StudyHistory h) => new()
        {
            Id = h.Id,
            StudentId = h.StudentId,
            SubjectId = h.SubjectId,
            LessonId = h.LessonId,
            Score = h.Score,
            StudyTime = h.StudyTime,
            StudyDate = h.StudyDate,
            Result = h.Result,
            Difficulty = h.Difficulty
        };

        private static ServiceResponse<StudyHistoryDto> MapResponse(int statusCode, StudyHistory? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<StudyHistoryDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Registro de historial {action} correctamente."
                    };

                case 50240:
                    return new ServiceResponse<StudyHistoryDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50241:
                    return new ServiceResponse<StudyHistoryDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la materia indicada." };

                case 50242:
                    return new ServiceResponse<StudyHistoryDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la leccion indicada." };

                case 50243:
                    return new ServiceResponse<StudyHistoryDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el registro de historial indicado." };

                default:
                    return new ServiceResponse<StudyHistoryDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}