using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{

    public interface IRoutineLogService
    {
        Task<ServiceResponse<RoutineLogDto>> CreateAsync(int requestingUserId, CreateRoutineLogDto request);
        Task<ServiceResponse<List<RoutineLogDto>>> GetByStudentAsync(int requestingUserId, int studentId, DateTime? fromDate, DateTime? toDate);
        Task<ServiceResponse<List<RoutineLogDto>>> GetByDetailAsync(int requestingUserId, int routineDetailId);
        Task<ServiceResponse<RoutineLogDto>> UpdateAsync(int requestingUserId, int id, UpdateRoutineLogDto request);
    }
}
