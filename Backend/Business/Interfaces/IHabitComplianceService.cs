using Business.DTOs;
using Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IHabitComplianceService
    {
        Task<ServiceResponse<List<HabitComplianceDto>>> GetByHabitAsync(int habitId);
        Task<ServiceResponse<HabitComplianceDto>> GetByIdAsync(int id);
        Task<ServiceResponse<HabitComplianceDto>> CreateAsync(CreateHabitComplianceDto request);
        Task<ServiceResponse<HabitComplianceDto>> UpdateAsync(int id, UpdateHabitComplianceDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
