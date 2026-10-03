using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IPecsCardRepository
    {
        Task<RepositoryResponse<PecsCard>> CreateAsync(PecsCard card);
        Task<RepositoryResponse<List<PecsCard>>> GetByBoardAsync(int boardId);
        Task<RepositoryResponse<PecsCardWithStudent>> GetByIdWithStudentAsync(int id);
        Task<RepositoryResponse<PecsCard>> UpdateAsync(int id, PecsCard card);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}
