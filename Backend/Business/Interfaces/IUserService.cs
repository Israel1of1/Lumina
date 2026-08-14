using Business.DTOs;
using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse<PagedResponse<IEnumerable<User>>>> GetAllAsync(PaginationParams pagination);
        Task<ServiceResponse<User>> GetByIdAsync(int id);
        Task<ServiceResponse<User>> UpdateAsync(int id, UpdateUserDto user);
    }
}
