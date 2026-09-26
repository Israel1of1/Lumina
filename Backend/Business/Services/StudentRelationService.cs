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

    public class StudentRelationService : IStudentRelationService
    {
        private readonly IStudentRelationRepository _relationRepository;

        public StudentRelationService(IStudentRelationRepository relationRepository)
        {
            _relationRepository = relationRepository;
        }

        public async Task<ServiceResponse<List<StudentRelationDto>>> GetByStudentAsync(int studentId, bool onlyActive)
        {
            var repoResponse = await _relationRepository.GetByStudentAsync(studentId, onlyActive);

            if (repoResponse.OperationStatusCode == 50170)
                return new ServiceResponse<List<StudentRelationDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

            if (repoResponse.OperationStatusCode != 0)
                return new ServiceResponse<List<StudentRelationDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar las relaciones." };

            return new ServiceResponse<List<StudentRelationDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Relaciones obtenidas correctamente."
            };
        }

        public async Task<ServiceResponse<List<MyStudentDto>>> GetMyStudentsAsync(int entityId, string entityType)
        {
            var repoResponse = await _relationRepository.GetActiveStudentsForEntityAsync(entityId, entityType);

            if (repoResponse.OperationStatusCode != 0)
                return new ServiceResponse<List<MyStudentDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar tus estudiantes." };

            return new ServiceResponse<List<MyStudentDto>>
            {
                Data = repoResponse.Data.Select(s => new MyStudentDto
                {
                    RelationId = s.RelationId,
                    RelationType = s.RelationType,
                    Id = s.Id,
                    GroupId = s.GroupId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    UniqueNumber = s.UniqueNumber,
                    BirthDate = s.BirthDate,
                    Gender = s.Gender,
                    LanguageLevel = s.LanguageLevel
                }).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Estudiantes obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<StudentRelationDto>> CreateAsync(CreateStudentRelationDto request)
        {
            var repoResponse = await _relationRepository.CreateAsync(
                request.EntityId, request.EntityType.ToUpperInvariant(), request.StudentId, request.RelationType.ToUpperInvariant(), request.AssignedAt);

            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creada");
        }

        public async Task<ServiceResponse<StudentRelationDto>> EndAsync(int id)
        {
            var repoResponse = await _relationRepository.EndAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "finalizada");
        }

        private static StudentRelationDto MapDto(EntityStudentRelation r) => new()
        {
            Id = r.Id,
            EntityId = r.EntityId,
            EntityType = r.EntityType,
            StudentId = r.StudentId,
            RelationType = r.RelationType,
            IsActive = r.IsActive,
            AssignedAt = r.AssignedAt,
            EndDate = r.EndDate,
            EntityFirstName = r.EntityFirstName,
            EntityLastName = r.EntityLastName
        };

        private static ServiceResponse<StudentRelationDto> MapResponse(int statusCode, EntityStudentRelation? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<StudentRelationDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Relacion {action} correctamente." };

                case 50170:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el estudiante indicado." };

                case 50190:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la relacion indicada." };

                case 50191:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el docente/tutor indicado." };

                case 50193:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorValidation, Message = "El tipo de relacion no es compatible con el tipo de entidad." };

                case 50194:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.Conflict, Message = "Ya existe esa relacion activa para este estudiante." };

                case 50195:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.Conflict, Message = "Este estudiante ya tiene un tutor primario activo. Finaliza esa relacion antes de crear otra." };

                default:
                    return new ServiceResponse<StudentRelationDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}
