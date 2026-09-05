using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ITeacherRepository
    {
        Task<RepositoryResponse<Teacher>> GetByUserIdAsync(int userId);
        Task<RepositoryResponse<Teacher>> UpdateProfileAsync(int userId, Teacher profile);
        Task<RepositoryResponse<Teacher>> PatchProfileAsync(int userId, Teacher profile);

        Task<RepositoryResponse<(List<TeacherWithAccount> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, string? status);
        Task<RepositoryResponse<Teacher>> DeactivateAsync(int teacherId, string? reason);
    }
}
