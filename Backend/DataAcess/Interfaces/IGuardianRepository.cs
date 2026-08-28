using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IGuardianRepository
    {
        Task<RepositoryResponse<Guardian>> GetByUserIdAsync(int userId);
        Task<RepositoryResponse<Guardian>> UpdateProfileAsync(int userId, Guardian profile);
        Task<RepositoryResponse<(List<Guardian> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, string? status);
        Task<RepositoryResponse<EntityStatusResult>> DeactivateAsync(int guardianId, string? reason);
    }
}

