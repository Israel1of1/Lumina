using Business.DTOs;
using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ILessonService
    {
        Task<ServiceResponse<PagedResponse<IEnumerable<Lesson>>>> GetAllAsync(PaginationParams pagination);
        Task<ServiceResponse<Lesson>> GetByIdAsync(int id);
        Task<ServiceResponse<Lesson>> CreateAsync(CreateLessonDto newLesson);
        Task<ServiceResponse<Lesson>> UpdateAsync(int id, UpdateLessonDto lesson);
    }
}
