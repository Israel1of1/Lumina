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
    public class StudentInterestService : IStudentInterestService
    {
        private readonly IStudentInterestRepository _studentInterestRepository;

        public StudentInterestService(IStudentInterestRepository studentInterestRepository)
        {
            _studentInterestRepository = studentInterestRepository;
        }

        public async Task<ServiceResponse<List<StudentInterestDto>>> GetByStudentAsync(int studentId)
        {
            var repoResponse = await _studentInterestRepository.GetByStudentAsync(studentId);

            if (repoResponse.OperationStatusCode == 50200)
            {
                return new ServiceResponse<List<StudentInterestDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<List<StudentInterestDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar los intereses." };
            }

            return new ServiceResponse<List<StudentInterestDto>>
            {
                Data = repoResponse.Data!.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Intereses obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<StudentInterestDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _studentInterestRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<StudentInterestDto>> CreateAsync(CreateStudentInterestDto request)
        {
            var interest = new StudentInterest
            {
                StudentId = request.StudentId,
                Name = request.Name,
                Description = request.Description
            };

            var repoResponse = await _studentInterestRepository.CreateAsync(interest);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<StudentInterestDto>> UpdateAsync(int id, UpdateStudentInterestDto request)
        {
            var interest = new StudentInterest
            {
                Name = request.Name,
                Description = request.Description
            };

            var repoResponse = await _studentInterestRepository.UpdateAsync(id, interest);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var repoResponse = await _studentInterestRepository.DeleteAsync(id);

            if (repoResponse.OperationStatusCode == 50201)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el interes indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<bool> { Data = false, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al eliminar el interes." };
            }

            return new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Interes eliminado correctamente." };
        }

        private static StudentInterestDto MapDto(StudentInterest i) => new()
        {
            Id = i.Id,
            StudentId = i.StudentId,
            Name = i.Name,
            Description = i.Description,
            CreatedAt = i.CreatedAt,
            UpdatedAt = i.UpdatedAt
        };

        private static ServiceResponse<StudentInterestDto> MapResponse(int statusCode, StudentInterest? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<StudentInterestDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Interes {action} correctamente."
                    };

                case 50200:
                    return new ServiceResponse<StudentInterestDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50201:
                    return new ServiceResponse<StudentInterestDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el interes indicado." };

                default:
                    return new ServiceResponse<StudentInterestDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}