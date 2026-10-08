using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("by-group/{groupId:int}")]
        [Authorize(Roles = "TEACHER")]
        public async Task<IActionResult> GetByGroup(int groupId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] bool onlyActive = true)
        {
            var result = await _studentService.GetByGroupAsync(groupId, pageNumber, pageSize, onlyActive);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "TEACHER")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _studentService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUTION,TEACHER")]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto request, [FromServices] Business.Interfaces.ITeacherService teacherService)
        {
            int? requestingTeacherId = null;

            if (User.IsInRole("DOCENTE") && !User.IsInRole("INSTITUCION"))
            {
                var profile = await teacherService.GetMyProfileAsync(GetCurrentUserId());
                if (!profile.IsSuccess)
                    return NotFound(profile);

                requestingTeacherId = profile.Data!.Id;
            }

            var result = await _studentService.CreateAsync(request, requestingTeacherId);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentDto request)
        {
            var result = await _studentService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}/active")]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> SetActive(int id, [FromBody] SetActiveDto request)
        {
            var result = await _studentService.SetActiveAsync(id, request.IsActive);
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
        private int GetCurrentUserId()
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            return int.Parse(subject!);
        }
    }
}
