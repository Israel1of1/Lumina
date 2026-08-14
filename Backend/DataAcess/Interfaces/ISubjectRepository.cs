
using Core.Common;
using Core.Entities;

namespace DataAccess.Interfaces
{
    public interface ISubjectRepository
    {
        Task<RepositoryResponse<PagedResponse<IEnumerable<Subject>>>> GetAllAsync(PaginationParams pagination);
        Task<RepositoryResponse<Subject>> GetByIdAsync(int id);
        Task<RepositoryResponse<Subject>> GetByNameAsync(string name);
        Task<RepositoryResponse<Subject>> AddAsync(Subject subject);
        Task<RepositoryResponse<Subject>> UpdateAsync(int id, Subject subject);
    }
}