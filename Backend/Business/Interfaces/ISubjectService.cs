
using Business.DTOs;
using Core.Common;
using Core.Entities;

namespace Business.Interfaces
{
    public interface ISubjectService
    {
        Task<ServiceResponse<PagedResponse<IEnumerable<Subject>>>> GetAllAsync(PaginationParams pagination);
        Task<ServiceResponse<Subject>> GetByIdAsync(int id);
        Task<ServiceResponse<Subject>> GetByNameAsync(string name);
        Task<ServiceResponse<Subject>> CreateAsync(CreateSubjectDto newSubject);
        Task<ServiceResponse<Subject>> UpdateAsync(int id, UpdateSubjectDto subject);
    }
}