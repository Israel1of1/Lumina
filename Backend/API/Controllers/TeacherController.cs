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
    [Route("api/teachers")]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("me")]
        [Authorize(Roles = "DOCENTE")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _teacherService.GetMyProfileAsync(GetCurrentUserId());
            return MapResponse(result);
        }

  

        [HttpGet]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? status = null)
        {
            var result = await _teacherService.GetAllAsync(pageNumber, pageSize, status);
            return MapResponse(result);
        }

        [HttpPut("me")]
        [Authorize(Roles = "DOCENTE")]
        public async Task<IActionResult> UpdateTeacherProfile([FromBody] UpdateTeacherProfileDto request)
        {
            var result = await _teacherService.UpdateMyProfileAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        [HttpPatch("me")]
        [Authorize(Roles = "DOCENTE")]
        public async Task<IActionResult> PatchMyProfile([FromBody] PatchTeacherProfileDto request)
        {
            var result = await _teacherService.PatchMyProfileAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        private int GetCurrentUserId()
        {
            var subject = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            return int.Parse(subject!);
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
