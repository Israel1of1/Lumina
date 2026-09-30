using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/student-interests")]
    [Authorize]
    public class StudentInterestController : ControllerBase
    {
        private readonly IStudentInterestService _studentInterestService;

        public StudentInterestController(IStudentInterestService studentInterestService)
        {
            _studentInterestService = studentInterestService;
        }

        [HttpGet("by-student/{studentId:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _studentInterestService.GetByStudentAsync(studentId);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentInterestService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Create([FromBody] CreateStudentInterestDto request)
        {
            var result = await _studentInterestService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentInterestDto request)
        {
            var result = await _studentInterestService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "INSTITUCION,TUTOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentInterestService.DeleteAsync(id);
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