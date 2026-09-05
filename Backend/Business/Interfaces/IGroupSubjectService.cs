using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGroupSubjectService
    {
        Task<ServiceResponse<List<GroupSubjectDto>>> GetByGroupAsync(int groupId);
        Task<ServiceResponse<List<GroupSubjectDto>>> GetByTeacherAsync(int teacherId);
        Task<ServiceResponse<GroupSubjectDto>> CreateAsync(CreateGroupSubjectDto request);
        Task<ServiceResponse<GroupSubjectDto>> SetActiveAsync(int id, bool isActive);
    }
}
