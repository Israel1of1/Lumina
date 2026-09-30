using Business.DTOs;
using Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentInterestService
    {
        Task<ServiceResponse<List<StudentInterestDto>>> GetByStudentAsync(int studentId);
        Task<ServiceResponse<StudentInterestDto>> GetByIdAsync(int id);
        Task<ServiceResponse<StudentInterestDto>> CreateAsync(CreateStudentInterestDto request);
        Task<ServiceResponse<StudentInterestDto>> UpdateAsync(int id, UpdateStudentInterestDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}