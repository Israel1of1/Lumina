using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudentRelationService
    {
        Task<ServiceResponse<List<StudentRelationDto>>> GetByStudentAsync(int studentId, bool onlyActive);
        Task<ServiceResponse<List<MyStudentDto>>> GetMyStudentsAsync(int entityId, string entityType);
        Task<ServiceResponse<StudentRelationDto>> CreateAsync(CreateStudentRelationDto request);
        Task<ServiceResponse<StudentRelationDto>> EndAsync(int id);
    }
}
