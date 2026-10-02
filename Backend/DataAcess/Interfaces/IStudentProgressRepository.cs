using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStudentProgressRepository
    {
        Task<RepositoryResponse<List<StudentProgress>>> GetByStudentAsync(int studentId);
        Task<RepositoryResponse<StudentProgress>> GetByIdAsync(int id);
        Task<RepositoryResponse<StudentProgress>> CreateAsync(StudentProgress progress);
        Task<RepositoryResponse<StudentProgress>> UpdateAsync(int id, StudentProgress progress);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}