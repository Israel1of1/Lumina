using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IAccessControlRepository
    {
        Task<bool> HasStudentAccessAsync(int userId, int studentId);
    }
}
