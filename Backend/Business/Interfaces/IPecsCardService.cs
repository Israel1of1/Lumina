using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPecsCardService
    {
        Task<ServiceResponse<PecsCardDto>> CreateAsync(int requestingUserId, CreatePecsCardDto request);
        Task<ServiceResponse<List<PecsCardDto>>> GetByBoardAsync(int requestingUserId, int boardId);
        Task<ServiceResponse<PecsCardDto>> UpdateAsync(int requestingUserId, int id, UpdatePecsCardDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int requestingUserId, int id);
    }
}
