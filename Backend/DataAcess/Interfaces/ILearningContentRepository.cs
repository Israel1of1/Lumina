using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ILearningContentRepository
    {
        Task<RepositoryResponse<(List<LearningContent> Items, int TotalRecords)>> GetAllAsync(LearningContentFilter filter);
        Task<RepositoryResponse<LearningContent>> GetByIdAsync(int id);
        Task<RepositoryResponse<LearningContent>> CreateAsync(LearningContent content);
        Task<RepositoryResponse<LearningContent>> UpdateAsync(int id, LearningContent content);
        Task<RepositoryResponse<LearningContent>> SetActiveAsync(int id, bool isActive);
    }
}
