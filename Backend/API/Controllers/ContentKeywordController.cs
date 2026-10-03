using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/learning-contents/{contentId:int}/keywords")]
    [Authorize]
    public class ContentKeywordController : BaseApiController
    {
        private readonly IContentKeywordService _contentKeywordService;

        public ContentKeywordController(IContentKeywordService contentKeywordService)
        {
            _contentKeywordService = contentKeywordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByContent(int contentId)
            => MapResponse(await _contentKeywordService.GetByContentAsync(contentId));

        // Asocia una palabra clave al contenido por keywordId o por name se crea si no existe 
        [HttpPost]
        [Authorize(Roles = "INSTITUCION,DOCENTE")]
        public async Task<IActionResult> Add(int contentId, [FromBody] AddContentKeywordDto request)
            => MapResponse(await _contentKeywordService.AddAsync(contentId, request));

        [HttpDelete("{keywordId:int}")]
        [Authorize(Roles = "INSTITUCION,DOCENTE")]
        public async Task<IActionResult> Remove(int contentId, int keywordId)
            => MapResponse(await _contentKeywordService.RemoveAsync(contentId, keywordId));
    }
}

