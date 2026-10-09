using Business.DTOs;
using Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentHabitService
    {
        Task<ServiceResponse<List<StudentHabitDto>>> GetByStudentAsync(int studentId);
        Task<ServiceResponse<StudentHabitDto>> GetByIdAsync(int id);
        Task<ServiceResponse<StudentHabitDto>> CreateAsync(CreateStudentHabitDto request);
        Task<ServiceResponse<StudentHabitDto>> UpdateAsync(int id, UpdateStudentHabitDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}