using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ILessonRepository
    {
        Task<RepositoryResponse<PagedResponse<IEnumerable<Lesson>>>> GetAllAsync(PaginationParams pagination);
        Task<RepositoryResponse<Lesson>> GetByIdAsync(int id);
        Task<RepositoryResponse<Lesson>> AddAsync(Lesson lesson);
        Task<RepositoryResponse<Lesson>> UpdateAsync(int id, Lesson lesson);
    }
}

