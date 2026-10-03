using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPecsBoardRepository
    {
        Task<RepositoryResponse<PecsBoard>> CreateAsync(PecsBoard board);
        Task<RepositoryResponse<List<PecsBoard>>> GetByStudentAsync(int studentId);
        Task<RepositoryResponse<PecsBoard>> GetByIdAsync(int id);
        Task<RepositoryResponse<PecsBoard>> UpdateAsync(int id, PecsBoard board);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}
