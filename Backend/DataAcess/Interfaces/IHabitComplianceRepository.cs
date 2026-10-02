using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IHabitComplianceRepository
    {
        Task<RepositoryResponse<List<HabitCompliance>>> GetByHabitAsync(int habitId);
        Task<RepositoryResponse<HabitCompliance>> GetByIdAsync(int id);
        Task<RepositoryResponse<HabitCompliance>> CreateAsync(HabitCompliance compliance);
        Task<RepositoryResponse<HabitCompliance>> UpdateAsync(int id, HabitCompliance compliance);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}