using Business.DTOs;
using Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentProgressService
    {
        Task<ServiceResponse<List<StudentProgressDto>>> GetByStudentAsync(int studentId);
        Task<ServiceResponse<StudentProgressDto>> GetByIdAsync(int id);
        Task<ServiceResponse<StudentProgressDto>> CreateAsync(CreateStudentProgressDto request);
        Task<ServiceResponse<StudentProgressDto>> UpdateAsync(int id, UpdateStudentProgressDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}