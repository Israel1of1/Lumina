using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentService
    {
        Task<ServiceResponse<PagedResultDto<StudentDto>>> GetByGroupAsync(int groupId, int pageNumber, int pageSize, bool onlyActive);
        Task<ServiceResponse<StudentDto>> GetByIdAsync(int id);
        Task<ServiceResponse<StudentDto>> CreateAsync(CreateStudentDto request);
        Task<ServiceResponse<StudentDto>> UpdateAsync(int id, UpdateStudentDto request);
        Task<ServiceResponse<StudentDto>> SetActiveAsync(int id, bool isActive);
    }
}
