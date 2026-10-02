using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{

    public interface IRoutineLogRepository
    {
        Task<RepositoryResponse<RoutineLog>> CreateAsync(RoutineLog log);
        Task<RepositoryResponse<List<RoutineLog>>> GetByStudentAsync(int studentId, DateTime? fromDate, DateTime? toDate);
        Task<RepositoryResponse<List<RoutineLog>>> GetByDetailAsync(int routineDetailId);
        Task<RepositoryResponse<RoutineLog>> GetByIdAsync(int id);
        Task<RepositoryResponse<RoutineLog>> UpdateAsync(int id, RoutineLog log);
    }

}
