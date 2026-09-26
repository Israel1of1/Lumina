using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/group-subjects")]
    [Authorize]
    public class GroupSubjectController : ControllerBase
    {
        private readonly IGroupSubjectService _groupSubjectService;

        public GroupSubjectController(IGroupSubjectService groupSubjectService)
        {
            _groupSubjectService = groupSubjectService;
        }

        [HttpGet("by-group/{groupId:int}")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> GetByGroup(int groupId)
        {
            var result = await _groupSubjectService.GetByGroupAsync(groupId);
            return MapResponse(result);
        }

        [HttpGet("by-teacher/{teacherId:int}")]
        [Authorize(Roles = "INSTITUCION,DOCENTE")]
        public async Task<IActionResult> GetByTeacher(int teacherId)
        {
            var result = await _groupSubjectService.GetByTeacherAsync(teacherId);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Create([FromBody] CreateGroupSubjectDto request)
        {
            var result = await _groupSubjectService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}/active")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> SetActive(int id, [FromBody] SetActiveDto request)
        {
            var result = await _groupSubjectService.SetActiveAsync(id, request.IsActive);
            return MapResponse(result);
        }

        private IActionResult MapResponse<T>(ServiceResponse<T> result)
        {
            if (result.IsSuccess)
                return Ok(result);

            return result.MessageCodes switch
            {
                MessageCodes.ErrorValidation => BadRequest(result),
                MessageCodes.Unauthorized => Unauthorized(result),
                MessageCodes.NotFound => NotFound(result),
                MessageCodes.Conflict => Conflict(result),
                _ => StatusCode(500, result)
            };
        }
    }
}
