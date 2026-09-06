using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IClassGroupService
    {
        Task<ServiceResponse<PagedResultDto<ClassGroupDto>>> GetAllAsync(int pageNumber, int pageSize, bool? isActive);
        Task<ServiceResponse<ClassGroupDto>> GetByIdAsync(int id);
        Task<ServiceResponse<ClassGroupDto>> CreateAsync(CreateClassGroupDto request);
        Task<ServiceResponse<ClassGroupDto>> UpdateAsync(int id, UpdateClassGroupDto request);
        Task<ServiceResponse<ClassGroupDto>> SetActiveAsync(int id, bool isActive);
    }
}
