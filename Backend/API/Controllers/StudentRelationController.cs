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
    [Route("api/student-relations")]
    [Authorize]
    public class StudentRelationController : ControllerBase
    {
        private readonly IStudentRelationService _relationService;

        public StudentRelationController(IStudentRelationService relationService)
        {
            _relationService = relationService;
        }

        [HttpGet("by-student/{studentId:int}")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> GetByStudent(int studentId, [FromQuery] bool onlyActive = true)
        {
            var result = await _relationService.GetByStudentAsync(studentId, onlyActive);
            return MapResponse(result);
        }

        /// <summary>Estudiantes activos vinculados al docente autenticado.</summary>
        [HttpGet("my-students")]
        [Authorize(Roles = "DOCENTE")]
        public async Task<IActionResult> GetMyStudentsAsTeacher([FromServices] Business.Interfaces.ITeacherService teacherService)
        {
            var profile = await teacherService.GetMyProfileAsync(GetCurrentUserId());
            if (!profile.IsSuccess)
                return NotFound(profile);

            var result = await _relationService.GetMyStudentsAsync(profile.Data!.Id, "TEACHER");
            return MapResponse(result);
        }

        /// <summary>Estudiantes activos vinculados al tutor autenticado.</summary>
        [HttpGet("my-wards")]
        [Authorize(Roles = "TUTOR")]
        public async Task<IActionResult> GetMyStudentsAsGuardian([FromServices] Business.Interfaces.IGuardianService guardianService)
        {
            var profile = await guardianService.GetMyProfileAsync(GetCurrentUserId());
            if (!profile.IsSuccess)
                return NotFound(profile);

            var result = await _relationService.GetMyStudentsAsync(profile.Data!.Id, "GUARDIAN");
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Create([FromBody] CreateStudentRelationDto request)
        {
            var result = await _relationService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}/end")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> End(int id)
        {
            var result = await _relationService.EndAsync(id);
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
