using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStudyHistoryRepository
    {
        Task<RepositoryResponse<List<StudyHistory>>> GetByStudentAsync(int studentId);
        Task<RepositoryResponse<StudyHistory>> GetByIdAsync(int id);
        Task<RepositoryResponse<StudyHistory>> CreateAsync(StudyHistory history);
        Task<RepositoryResponse<StudyHistory>> UpdateAsync(int id, StudyHistory history);
        Task<RepositoryResponse<bool>> DeleteAsync(int id);
    }
}