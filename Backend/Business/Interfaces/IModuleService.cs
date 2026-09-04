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
    public interface IModuleService
    {
        Task<ServiceResponse<PagedResponse<IEnumerable<Module>>>> GetAllAsync(PaginationParams pagination);
        Task<ServiceResponse<Module>> GetByIdAsync(int id);
        Task<ServiceResponse<Module>> CreateAsync(CreateModuleDto newModule);
        Task<ServiceResponse<Module>> UpdateAsync(int id, UpdateModuleDto module);
    }

}
