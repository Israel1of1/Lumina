using Core.Common;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IContentKeywordRepository
    {
        Task<RepositoryResponse<List<ContentKeyWord>>> GetByContentAsync(int contentId);
        Task<RepositoryResponse<ContentKeyWord>> AddAsync(int contentId, int? keywordId, string? keywordName);
        Task<RepositoryResponse<bool>> RemoveAsync(int contentId, int keywordId);
    }
}
