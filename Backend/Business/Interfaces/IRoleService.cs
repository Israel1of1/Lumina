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
    public interface IRoleService
    {
        Task<ServiceResponse<IEnumerable<Role>>> GetAllAsync();
        Task<ServiceResponse<Role>> GetByIdAsync(int id);
        Task<ServiceResponse<Role>> CreateAsync(CreateRoleDto newRole);
        Task<ServiceResponse<Role>> UpdateAsync(int id, UpdateRoleDto role);
    }
}
