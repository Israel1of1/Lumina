using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IStudentRepository
    {
        Task<RepositoryResponse<(List<Student> Items, int TotalRecords)>> GetByGroupAsync(int groupId, int pageNumber, int pageSize, bool onlyActive);
        Task<RepositoryResponse<Student>> GetByIdAsync(int id);
        Task<RepositoryResponse<Student>> CreateAsync(Student student);
        Task<RepositoryResponse<Student>> UpdateAsync(int id, Student student);
        Task<RepositoryResponse<Student>> SetActiveAsync(int id, bool isActive);
    }
}
