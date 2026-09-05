using Core.Common;
using Core.Entities;

namespace DataAccess.Interfaces
{
    public interface ILessonStepRepository
    {
        Task<RepositoryResponse<PagedResponse<IEnumerable<LessonStep>>>> GetAllAsync(PaginationParams pagination);
        Task<RepositoryResponse<LessonStep>> GetByIdAsync(int id);
        Task<RepositoryResponse<LessonStep>> AddAsync(LessonStep lessonStep);
        Task<RepositoryResponse<LessonStep>> UpdateAsync(int id, LessonStep lessonStep);
    }
}