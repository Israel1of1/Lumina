using Core.Common;
using Core.Entities;

namespace DataAccess.Interfaces
{
    public interface IRoleRepository
    {
        Task<RepositoryResponse<IEnumerable<Role>>> GetAllAsync();
        Task<RepositoryResponse<Role>> GetByIdAsync(int id);
        Task<RepositoryResponse<Role>> AddAsync(Role role);
        Task<RepositoryResponse<Role>> UpdateAsync(int id, Role role);
    }
}