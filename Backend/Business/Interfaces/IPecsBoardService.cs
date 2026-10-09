using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPecsBoardService
    {
        Task<ServiceResponse<PecsBoardDto>> CreateAsync(int requestingUserId, CreatePecsBoardDto request);
        Task<ServiceResponse<List<PecsBoardDto>>> GetByStudentAsync(int requestingUserId, int studentId);
        Task<ServiceResponse<PecsBoardDto>> UpdateAsync(int requestingUserId, int id, UpdatePecsBoardDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int requestingUserId, int id);
    }
}
