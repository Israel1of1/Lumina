using Business.DTOs;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/learning-contents")]
    [Authorize]
    public class LearningContentController : BaseApiController
    {
        private const string ContentEditors = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR";
        private readonly ILearningContentService _contentService;

        public LearningContentController(ILearningContentService contentService)
        {
            _contentService = contentService;
        }

        // El Tutor solo consulta contenido activo; Docente e Institucion pueden ver tambien los inactivos
        private bool CanSeeInactive => User.IsInRole("INSTITUTION") || User.IsInRole("INSTITUCION") || User.IsInRole("TEACHER") || User.IsInRole("DOCENTE");

        // Base de Conocimientos es decir lista y busca contenido filtros opcionales:
        // lessonId, subjectId, type, level, search (titulo/descripcion/palabras clave), isActive.

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] LearningContentFilterDto filter)
        {
            if (!CanSeeInactive)
                filter.IsActive = true;

            return MapResponse(await _contentService.GetAllAsync(filter));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => MapResponse(await _contentService.GetByIdAsync(id, CanSeeInactive));

        [HttpPost]
        [Authorize(Roles = ContentEditors)]
        public async Task<IActionResult> Create([FromBody] CreateLearningContentDto request)
            => MapResponse(await _contentService.CreateAsync(request));

        [HttpPut("{id:int}")]
        [Authorize(Roles = ContentEditors)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLearningContentDto request)
            => MapResponse(await _contentService.UpdateAsync(id, request));

        [HttpPatch("{id:int}/active")]
        [Authorize(Roles = ContentEditors)]
        public async Task<IActionResult> SetActive(int id, [FromBody] SetActiveDto request)
            => MapResponse(await _contentService.SetActiveAsync(id, request.IsActive));
    }
}
