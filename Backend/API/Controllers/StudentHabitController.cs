using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/student-habits")]
    [Authorize]
    public class StudentHabitController : ControllerBase
    {
        private readonly IStudentHabitService _studentHabitService;

        public StudentHabitController(IStudentHabitService studentHabitService)
        {
            _studentHabitService = studentHabitService;
        }

        [HttpGet("by-student/{studentId:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _studentHabitService.GetByStudentAsync(studentId);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentHabitService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Create([FromBody] CreateStudentHabitDto request)
        {
            var result = await _studentHabitService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentHabitDto request)
        {
            var result = await _studentHabitService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentHabitService.DeleteAsync(id);
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