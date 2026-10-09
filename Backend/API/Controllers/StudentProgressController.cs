using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/student-progresses")]
    [Authorize]
    public class StudentProgressController : ControllerBase
    {
        private readonly IStudentProgressService _studentProgressService;

        public StudentProgressController(IStudentProgressService studentProgressService)
        {
            _studentProgressService = studentProgressService;
        }

        [HttpGet("by-student/{studentId:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _studentProgressService.GetByStudentAsync(studentId);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentProgressService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> Create([FromBody] CreateStudentProgressDto request)
        {
            var result = await _studentProgressService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentProgressDto request)
        {
            var result = await _studentProgressService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentProgressService.DeleteAsync(id);
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