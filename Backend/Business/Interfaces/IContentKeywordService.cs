using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IContentKeywordService
    {
        Task<ServiceResponse<List<ContentKeywordDto>>> GetByContentAsync(int contentId);
        Task<ServiceResponse<ContentKeywordDto>> AddAsync(int contentId, AddContentKeywordDto request);
        Task<ServiceResponse<bool>> RemoveAsync(int contentId, int keywordId);
    }
}
