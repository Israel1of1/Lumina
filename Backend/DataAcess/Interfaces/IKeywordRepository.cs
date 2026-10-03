using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IKeywordRepository
    {
        Task<RepositoryResponse<(List<Keyword> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, string? search);
        Task<RepositoryResponse<Keyword>> GetByIdAsync(int id);
        Task<RepositoryResponse<Keyword>> CreateAsync(string name);
        Task<RepositoryResponse<Keyword>> UpdateAsync(int id, string name);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}
