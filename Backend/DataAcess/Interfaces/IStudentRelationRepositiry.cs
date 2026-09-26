using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStudentRelationRepository
    {
        Task<RepositoryResponse<List<EntityStudentRelation>>> GetByStudentAsync(int studentId, bool onlyActive);
        Task<RepositoryResponse<List<StudentForEntity>>> GetActiveStudentsForEntityAsync(int entityId, string entityType);
        Task<RepositoryResponse<EntityStudentRelation>> CreateAsync(int entityId, string entityType, int studentId, string relationType, DateTime? assignedAt);
        Task<RepositoryResponse<EntityStudentRelation>> EndAsync(int id);
    }
}
