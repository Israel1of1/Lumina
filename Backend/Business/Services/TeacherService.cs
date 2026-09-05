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

    public class TeacherService : ITeacherService
     {
            private readonly ITeacherRepository _teacherRepository;

            public TeacherService(ITeacherRepository teacherRepository)
            {
                _teacherRepository = teacherRepository;
            }

            public async Task<ServiceResponse<TeacherProfileDto>> GetMyProfileAsync(int userId)
            {
                var repoResponse = await _teacherRepository.GetByUserIdAsync(userId);
                return MapProfileResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
            }

            public async Task<ServiceResponse<TeacherProfileDto>> UpdateMyProfileAsync(int userId, UpdateTeacherProfileDto request)
            {
                var profile = new Teacher
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    NationalId = request.NationalId,
                    PersonalEmail = request.PersonalEmail,
                    Phone = request.Phone,
                    Address = request.Address,
                    City = request.City,
                    Photo = request.Photo,
                    Specialty = request.Specialty,
                    Degree = request.Degree
                };

                var repoResponse = await _teacherRepository.UpdateProfileAsync(userId, profile);
                return MapProfileResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
            }
        public async Task<ServiceResponse<TeacherProfileDto>> PatchMyProfileAsync(int userId, PatchTeacherProfileDto request)
        {
            var profile = new Teacher
            {
                FirstName = request.FirstName!,
                LastName = request.LastName!,
                NationalId = request.NationalId,
                PersonalEmail = request.PersonalEmail,
                Phone = request.Phone,
                Address = request.Address,
                City = request.City,
                Photo = request.Photo,
                Specialty = request.Specialty,
                Degree = request.Degree
            };

            var repoResponse = await _teacherRepository.PatchProfileAsync(userId, profile);
            return MapProfileResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<PagedResultDto<TeacherProfileDto>>> GetAllAsync(int pageNumber, int pageSize, string? status)
            {
                var repoResponse = await _teacherRepository.GetAllAsync(pageNumber, pageSize, status);

                if (repoResponse.OperationStatusCode != 0)
                {
                    return new ServiceResponse<PagedResultDto<TeacherProfileDto>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = "Ocurrio un error al consultar los docentes."
                    };
                }

                var (items, totalRecords) = repoResponse.Data;

                return new ServiceResponse<PagedResultDto<TeacherProfileDto>>
                {
                    Data = new PagedResultDto<TeacherProfileDto>
                    {
                        Items = items.Select(x => new TeacherProfileDto
                        {
                            Id = x.Teacher.Id,
                            FirstName = x.Teacher.FirstName,
                            LastName = x.Teacher.LastName,
                            NationalId = x.Teacher.NationalId,
                            PersonalEmail = x.Teacher.PersonalEmail,
                            Phone = x.Teacher.Phone,
                            Specialty = x.Teacher.Specialty,
                            Degree = x.Teacher.Degree,
                            EntityStatus = x.Teacher.EntityStatus,
                          
                        }).ToList(),
                        TotalRecords = totalRecords,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    },
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Docentes obtenidos correctamente."
                };
            }

            public async Task<ServiceResponse<EntityStatusDto>> DeactivateAsync(int teacherId, string? reason)
            {
                var repoResponse = await _teacherRepository.DeactivateAsync(teacherId, reason);

                switch (repoResponse.OperationStatusCode)
                {
                    case 0:
                        return new ServiceResponse<EntityStatusDto>
                        {
                            Data = new EntityStatusDto
                            {
                                Id = repoResponse.Data!.Id,
                                FirstName = repoResponse.Data.FirstName,
                                LastName = repoResponse.Data.LastName,
                                EntityStatus = repoResponse.Data.EntityStatus,
                                DismissalDate = repoResponse.Data.DismissalDate,
                                DismissalReason = repoResponse.Data.DismissalReason
                            },
                            IsSuccess = true,
                            MessageCodes = MessageCodes.Success,
                            Message = "Docente dado de baja correctamente."
                        };

                    case 5090:
                        return new ServiceResponse<EntityStatusDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = "No se encontro el docente indicado."
                        };

                    case 5093:
                        return new ServiceResponse<EntityStatusDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.Conflict,
                            Message = "Este docente ya se encuentra inactivo."
                        };

                    default:
                        return new ServiceResponse<EntityStatusDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = "Ocurrio un error inesperado al dar de baja al docente."
                        };
                }
            }

            private static ServiceResponse<TeacherProfileDto> MapProfileResponse(int statusCode, Teacher? data, string action)
            {
                switch (statusCode)
                {
                    case 0:
                        return new ServiceResponse<TeacherProfileDto>
                        {
                            Data = new TeacherProfileDto
                            {
                                Id = data!.Id,
                                FirstName = data.FirstName,
                                LastName = data.LastName,
                                NationalId = data.NationalId,
                                PersonalEmail = data.PersonalEmail,
                                Phone = data.Phone,
                                Address = data.Address,
                                City = data.City,
                                Photo = data.Photo,
                                Specialty = data.Specialty,
                                Degree = data.Degree,
                                EntityStatus = data.EntityStatus
                            },
                            IsSuccess = true,
                            MessageCodes = MessageCodes.Success,
                            Message = $"Perfil {action} correctamente."
                        };

                    case 5090:
                        return new ServiceResponse<TeacherProfileDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = "No se encontro un perfil de docente vinculado a esta cuenta."
                        };

                    case 5092:
                        return new ServiceResponse<TeacherProfileDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.Conflict,
                            Message = "Ya existe otro docente registrado con ese numero de identificacion."
                        };

                    default:
                        return new ServiceResponse<TeacherProfileDto>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = "Ocurrio un error inesperado."
                        };
                }
            }
        
    }
}
