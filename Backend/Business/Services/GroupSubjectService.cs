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
    public class GroupSubjectService : IGroupSubjectService
    {
        private readonly IGroupSubjectRepository _groupSubjectRepository;

        public GroupSubjectService(IGroupSubjectRepository groupSubjectRepository)
        {
            _groupSubjectRepository = groupSubjectRepository;
        }

        public async Task<ServiceResponse<List<GroupSubjectDto>>> GetByGroupAsync(int groupId)
        {
            var repoResponse = await _groupSubjectRepository.GetByGroupAsync(groupId);
            return MapListResponse(repoResponse.OperationStatusCode, repoResponse.Data);
        }

        public async Task<ServiceResponse<List<GroupSubjectDto>>> GetByTeacherAsync(int teacherId)
        {
            var repoResponse = await _groupSubjectRepository.GetByTeacherAsync(teacherId);
            return MapListResponse(repoResponse.OperationStatusCode, repoResponse.Data);
        }

        public async Task<ServiceResponse<GroupSubjectDto>> CreateAsync(CreateGroupSubjectDto request)
        {
            var repoResponse = await _groupSubjectRepository.CreateAsync(request.GroupId, request.SubjectId, request.TeacherId, request.AssignmentDate);
            return MapSingleResponse(repoResponse.OperationStatusCode, repoResponse.Data, "asignada");
        }

        public async Task<ServiceResponse<GroupSubjectDto>> SetActiveAsync(int id, bool isActive)
        {
            var repoResponse = await _groupSubjectRepository.SetActiveAsync(id, isActive);
            return MapSingleResponse(repoResponse.OperationStatusCode, repoResponse.Data, isActive ? "reactivada" : "finalizada");
        }

        private static GroupSubjectDto MapDto(GroupSubject g) => new()
        {
            Id = g.Id,
            GroupId = g.GroupId,
            SubjectId = g.SubjectId,
            TeacherId = g.TeacherId,
            SubjectName = g.SubjectName,
            TeacherFirstName = g.TeacherFirstName,
            TeacherLastName = g.TeacherLastName,
            GroupName = g.GroupName,
            IsActive = g.IsActive,
            AssignmentDate = g.AssignmentDate,
            EndDate = g.EndDate
        };

        private static ServiceResponse<List<GroupSubjectDto>> MapListResponse(int statusCode, List<GroupSubject> data)
        {
            if (statusCode == 50160)
                return new ServiceResponse<List<GroupSubjectDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el grupo indicado." };

            if (statusCode == 5090)
                return new ServiceResponse<List<GroupSubjectDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el docente indicado." };

            if (statusCode != 0)
                return new ServiceResponse<List<GroupSubjectDto>> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error al consultar las asignaciones." };

            return new ServiceResponse<List<GroupSubjectDto>>
            {
                Data = data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Asignaciones obtenidas correctamente."
            };
        }

        private static ServiceResponse<GroupSubjectDto> MapSingleResponse(int statusCode, GroupSubject? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<GroupSubjectDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Materia {action} correctamente." };

                case 50160:
                    return new ServiceResponse<GroupSubjectDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el grupo indicado." };

                case 50150:
                    return new ServiceResponse<GroupSubjectDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la materia indicada." };

                case 5090:
                    return new ServiceResponse<GroupSubjectDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro el docente indicado." };

                case 50180:
                    return new ServiceResponse<GroupSubjectDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.NotFound, Message = "No se encontro la asignacion indicada." };

                case 50181:
                    return new ServiceResponse<GroupSubjectDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.Conflict, Message = "Ya existe esa asignacion de materia, grupo y docente en esa fecha." };

                default:
                    return new ServiceResponse<GroupSubjectDto> { Data = null, IsSuccess = false, MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }
    }
}
