using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStudentHabitRepository
    {
        Task<RepositoryResponse<List<StudentHabit>>> GetByStudentAsync(int studentId);
        Task<RepositoryResponse<StudentHabit>> GetByIdAsync(int id);
        Task<RepositoryResponse<StudentHabit>> CreateAsync(StudentHabit habit);
        Task<RepositoryResponse<StudentHabit>> UpdateAsync(int id, StudentHabit habit);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}