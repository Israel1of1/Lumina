using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRoutineService
    {
        Task<ServiceResponse<RoutineDto>> CreateAsync(int requestingUserId, CreateRoutineDto request);
        Task<ServiceResponse<List<RoutineDto>>> GetByStudentAsync(int requestingUserId, int studentId, string? status);
        Task<ServiceResponse<RoutineDto>> GetByIdAsync(int requestingUserId, int id);
        Task<ServiceResponse<RoutineDto>> UpdateAsync(int requestingUserId, int id, UpdateRoutineDto request);
        Task<ServiceResponse<RoutineDto>> SetStatusAsync(int requestingUserId, int id, string status);
    }
}
