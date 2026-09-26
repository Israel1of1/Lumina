using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IClassGroupRepository
    {
        Task<RepositoryResponse<(List<ClassGroup> Items, int TotalRecords)>> GetAllAsync(int pageNumber, int pageSize, bool? isActive);
        Task<RepositoryResponse<ClassGroup>> GetByIdAsync(int id);
        Task<RepositoryResponse<ClassGroup>> CreateAsync(ClassGroup group);
        Task<RepositoryResponse<ClassGroup>> UpdateAsync(int id, ClassGroup group);
        Task<RepositoryResponse<ClassGroup>> SetActiveAsync(int id, bool isActive);
    }
}
