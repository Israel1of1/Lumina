using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ILearningContentService
    {
        Task<ServiceResponse<PagedResultDto<LearningContentDto>>> GetAllAsync(LearningContentFilterDto request);
        Task<ServiceResponse<LearningContentDto>> GetByIdAsync(int id, bool includeInactive);
        Task<ServiceResponse<LearningContentDto>> CreateAsync(CreateLearningContentDto request);
        Task<ServiceResponse<LearningContentDto>> UpdateAsync(int id, UpdateLearningContentDto request);
        Task<ServiceResponse<LearningContentDto>> SetActiveAsync(int id, bool isActive);
    }
}
