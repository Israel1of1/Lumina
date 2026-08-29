using Core.Common;
using Core.Entities;

namespace DataAccess.Interfaces
{
    public interface IModuleRepository
    {
        Task<RepositoryResponse<PagedResponse<IEnumerable<Module>>>> GetAllAsync(PaginationParams pagination);
        Task<RepositoryResponse<Module>> GetByIdAsync(int id);
        Task<RepositoryResponse<Module>> AddAsync(Module module);
        Task<RepositoryResponse<Module>> UpdateAsync(int id, Module module);
    }
}