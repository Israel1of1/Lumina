using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGuardianService
    {
        Task<ServiceResponse<GuardianProfileDto>> GetMyProfileAsync(int userId);
        Task<ServiceResponse<GuardianProfileDto>> UpdateMyProfileAsync(int userId, UpdateGuardianProfileDto request);
        
        Task<ServiceResponse<PagedResultDto<GuardianProfileDto>>> GetAllAsync(int pageNumber, int pageSize, string? status);
        Task<ServiceResponse<EntityStatusDto>> DeactivateAsync(int guardianId, string? reason);
    }
}
