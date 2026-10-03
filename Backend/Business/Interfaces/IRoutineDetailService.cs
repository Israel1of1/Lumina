using Business.DTOs;
using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRoutineDetailService
    {
        Task<ServiceResponse<RoutineDetailDto>> CreateAsync(int requestingUserId, CreateRoutineDetailDto request);
        Task<ServiceResponse<List<RoutineDetailDto>>> GetByRoutineAsync(int requestingUserId, int routineId);
        Task<ServiceResponse<RoutineDetailDto>> UpdateAsync(int requestingUserId, int id, UpdateRoutineDetailDto request);
       // Task<(RoutineDetail Detail, int StudentId, int OperationStatusCode)> GetByIdWithStudentAsync(int id);
        Task<ServiceResponse<bool>> DeleteAsync(int requestingUserId, int id);
    }
}
