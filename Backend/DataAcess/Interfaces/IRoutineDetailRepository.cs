using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRoutineDetailRepository
    {
        Task<RepositoryResponse<RoutineDetail>> CreateAsync(RoutineDetail detail);
        Task<RepositoryResponse<List<RoutineDetail>>> GetByRoutineAsync(int routineId);
        Task<RepositoryResponse<RoutineDetailWithStudent>> GetByIdWithStudentAsync(int id);
        Task<RepositoryResponse<RoutineDetail>> UpdateAsync(int id, RoutineDetail detail);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}
