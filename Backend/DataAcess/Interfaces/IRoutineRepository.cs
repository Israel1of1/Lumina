using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRoutineRepository
    {
        Task<RepositoryResponse<Routine>> CreateAsync(Routine routine);
        Task<RepositoryResponse<List<Routine>>> GetByStudentAsync(int studentId, string? status);
        Task<RepositoryResponse<Routine>> GetByIdAsync(int id);
        Task<RepositoryResponse<Routine>> UpdateAsync(int id, Routine routine);
        Task<RepositoryResponse<Routine>> SetStatusAsync(int id, string status);
    }
}
