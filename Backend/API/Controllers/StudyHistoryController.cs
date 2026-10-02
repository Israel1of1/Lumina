using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/study-history")]
    [Authorize]
    public class StudyHistoryController : ControllerBase
    {
        private readonly IStudyHistoryService _studyHistoryService;

        public StudyHistoryController(IStudyHistoryService studyHistoryService)
        {
            _studyHistoryService = studyHistoryService;
        }

        [HttpGet("by-student/{studentId:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR,DOCENTE")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _studyHistoryService.GetByStudentAsync(studentId);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studyHistoryService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Create([FromBody] CreateStudyHistoryDto request)
        {
            var result = await _studyHistoryService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudyHistoryDto request)
        {
            var result = await _studyHistoryService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studyHistoryService.DeleteAsync(id);
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