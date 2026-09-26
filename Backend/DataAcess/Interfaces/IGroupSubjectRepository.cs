using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IGroupSubjectRepository
    {
        Task<RepositoryResponse<List<GroupSubject>>> GetByGroupAsync(int groupId);
        Task<RepositoryResponse<List<GroupSubject>>> GetByTeacherAsync(int teacherId);
        Task<RepositoryResponse<GroupSubject>> CreateAsync(int groupId, int subjectId, int teacherId, System.DateTime? assignmentDate);
        Task<RepositoryResponse<GroupSubject>> SetActiveAsync(int id, bool isActive);
    }
}
