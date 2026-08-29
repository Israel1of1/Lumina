using Business.DTOs;
using Core.Common;
using Core.Entities;

namespace Business.Interfaces
{
    public interface ILessonStepService
    {
        Task<ServiceResponse<PagedResponse<IEnumerable<LessonStep>>>> GetAllAsync(PaginationParams pagination);
        Task<ServiceResponse<LessonStep>> GetByIdAsync(int id);
        Task<ServiceResponse<LessonStep>> CreateAsync(CreateLessonStepDto newLessonStep);
        Task<ServiceResponse<LessonStep>> UpdateAsync(int id, UpdateLessonStepDto lessonStep);
    }
}