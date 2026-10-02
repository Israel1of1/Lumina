using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStudentInterestRepository
    {
        Task<RepositoryResponse<List<StudentInterest>>> GetByStudentAsync(int studentId);
        Task<RepositoryResponse<StudentInterest>> GetByIdAsync(int id);
        Task<RepositoryResponse<StudentInterest>> CreateAsync(StudentInterest interest);
        Task<RepositoryResponse<StudentInterest>> UpdateAsync(int Id, StudentInterest interest);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}
