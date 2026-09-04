using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ILinkCodeRepository
    {
        Task<RepositoryResponse<LinkCodeInfo>> CreateForTeacherAsync(int teacherId, int? issuedById, DateTime? expiresAt);
        Task<RepositoryResponse<LinkCodeInfo>> CreateForGuardianAsync(int guardianId, int? issuedById, DateTime? expiresAt);
        Task<RepositoryResponse<bool>> RevokeAsync(string code);
    }
}
