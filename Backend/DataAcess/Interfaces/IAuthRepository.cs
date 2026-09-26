using Core;
using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IAuthRepository
    {

        Task<RepositoryResponse<User>> GetByEmailAsync(string email);
        Task<RepositoryResponse<LinkCodeInfo>> GetLinkCodeInfoAsync(string code);
        Task<RepositoryResponse<User>> RegisterWithLinkCodeAsync(string code, string email, string passwordHash);
        Task<RepositoryResponse<bool>> UpdatePasswordAsync(int userId, string newPasswordHash);
        Task<RepositoryResponse<User>> GetByIdAsync(int userId);
    }
}
