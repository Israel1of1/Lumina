using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITeacherService
    {

        Task<ServiceResponse<TeacherProfileDto>> GetMyProfileAsync(int userId);
        Task<ServiceResponse<TeacherProfileDto>> UpdateMyProfileAsync(int userId, UpdateTeacherProfileDto request);
        Task<ServiceResponse<TeacherProfileDto>> PatchMyProfileAsync(int userId, PatchTeacherProfileDto request);
        Task<ServiceResponse<PagedResultDto<TeacherProfileDto>>> GetAllAsync(int pageNumber, int pageSize, string? status);
        Task<ServiceResponse<EntityStatusDto>> DeactivateAsync(int teacherId, string? reason);
    }
}
