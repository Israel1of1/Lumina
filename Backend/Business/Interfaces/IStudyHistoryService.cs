using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IStudyHistoryService
    {
        Task<ServiceResponse<List<StudyHistoryDto>>> GetByStudentAsync(int studentId);
        Task<ServiceResponse<StudyHistoryDto>> GetByIdAsync(int id);
        Task<ServiceResponse<StudyHistoryDto>> CreateAsync(CreateStudyHistoryDto request);
        Task<ServiceResponse<StudyHistoryDto>> UpdateAsync(int id, UpdateStudyHistoryDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
