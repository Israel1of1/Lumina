using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserRoleRepository
    {
        Task<RepositoryResponse<IEnumerable<Role>>> GetRolesByUserIdAsync(int userId);
    }
}
