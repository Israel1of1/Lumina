using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IKeywordService
    {
        Task<ServiceResponse<PagedResultDto<KeywordDto>>> GetAllAsync(int pageNumber, int pageSize, string? search);
        Task<ServiceResponse<KeywordDto>> GetByIdAsync(int id);
        Task<ServiceResponse<KeywordDto>> CreateAsync(CreateKeywordDto request);
        Task<ServiceResponse<KeywordDto>> UpdateAsync(int id, UpdateKeywordDto request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
