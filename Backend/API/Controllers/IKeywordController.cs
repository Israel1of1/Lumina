using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/keywords")]
    [Authorize]
    public class KeywordController : BaseApiController
    {
        private readonly IKeywordService _keywordService;

        public KeywordController(IKeywordService keywordService)
        {
            _keywordService = keywordService;
        }

        //Lista/busca palabras clave util para autocompletar
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? search = null)
            => MapResponse(await _keywordService.GetAllAsync(pageNumber, pageSize, search));

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => MapResponse(await _keywordService.GetByIdAsync(id));

        [HttpPost]
        [Authorize(Roles = "INSTITUCION,DOCENTE")]
        public async Task<IActionResult> Create([FromBody] CreateKeywordDto request)
            => MapResponse(await _keywordService.CreateAsync(request));

        // Renombrar/eliminar afecta a todos los contenidos que la usan
        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateKeywordDto request)
            => MapResponse(await _keywordService.UpdateAsync(id, request));

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Delete(int id)
            => MapResponse(await _keywordService.DeleteAsync(id));
    }
}
