using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Common;

namespace DataAccess.Interfaces
{
    public  interface IUserRepository
    {
        Task<RepositoryResponse<PagedResponse<IEnumerable<User>>>> GetAllAsync(PaginationParams pagination);
        Task<RepositoryResponse<User>> GetByIdAsync(int id);
        Task<RepositoryResponse<User>> UpdateAsync(int id, User user);
        Task<RepositoryResponse<bool>> UpdateLastLoginAsync(int id);
    }
}
