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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ServiceResponse<PagedResultDto<StudentDto>>> GetByGroupAsync(int groupId, int pageNumber, int pageSize, bool onlyActive)
        {
            var repoResponse = await _studentRepository.GetByGroupAsync(groupId, pageNumber, pageSize, onlyActive);

            if (repoResponse.OperationStatusCode == 50160)
            {
                return new ServiceResponse<PagedResultDto<StudentDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el grupo indicado." };
            }

            if (repoResponse.OperationStatusCode != 0)
            {
                return new ServiceResponse<PagedResultDto<StudentDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar los estudiantes." };
            }

            var (items, totalRecords) = repoResponse.Data;

            return new ServiceResponse<PagedResultDto<StudentDto>>
            {
                Data = new PagedResultDto<StudentDto>
                {
                    Items = items.Select(MapDto).ToList(),
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Estudiantes obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<StudentDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _studentRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<StudentDto>> CreateAsync(CreateStudentDto request)
        {
            var student = new Student
            {
                GroupId = request.GroupId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UniqueNumber = request.UniqueNumber,
                BirthDate = request.BirthDate,
                Gender = request.Gender
            };

            var repoResponse = await _studentRepository.CreateAsync(student);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "matriculado");
        }

        public async Task<ServiceResponse<StudentDto>> UpdateAsync(int id, UpdateStudentDto request)
        {
            var student = new Student
            {
                GroupId = request.GroupId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UniqueNumber = request.UniqueNumber,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                LanguageLevel = request.LanguageLevel,
                ClinicalInfo = request.ClinicalInfo,
                Observations = request.Observations
            };

            var repoResponse = await _studentRepository.UpdateAsync(id, student);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<StudentDto>> SetActiveAsync(int id, bool isActive)
        {
            var repoResponse = await _studentRepository.SetActiveAsync(id, isActive);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, isActive ? "reactivado" : "desactivado");
        }

        private static StudentDto MapDto(Student s) => new()
        {
            Id = s.Id,
            GroupId = s.GroupId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            UniqueNumber = s.UniqueNumber,
            BirthDate = s.BirthDate,
            Gender = s.Gender,
            LanguageLevel = s.LanguageLevel,
            ClinicalInfo = s.ClinicalInfo,
            Observations = s.Observations,
            IsActive = s.IsActive,
            HasAccount = s.UserId != null
        };

        private static ServiceResponse<StudentDto> MapResponse(int statusCode, Student? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<StudentDto>
                    {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = $"Estudiante {action} correctamente."
                    };

                case 50170:
                    return new ServiceResponse<StudentDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50171:
                    return new ServiceResponse<StudentDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "El grupo indicado no existe." };

                case 50172:
                    return new ServiceResponse<StudentDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.Conflict, Message = "El grupo ya alcanzo el limite de 10 estudiantes." };

                case 50173:
                    return new ServiceResponse<StudentDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.Conflict, Message = "Ya existe un estudiante con ese numero unico." };

                default:
                    return new ServiceResponse<StudentDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}
