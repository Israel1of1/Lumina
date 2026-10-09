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
    [Route("api/routines")]
    [Authorize]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineService _routineService;

        public RoutineController(IRoutineService routineService)
        {
            _routineService = routineService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoutineDto request)
        {
            var result = await _routineService.CreateAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        [HttpGet("by-student/{studentId:int}")]
        public async Task<IActionResult> GetByStudent(int studentId, [FromQuery] string? status = null)
        {
            var result = await _routineService.GetByStudentAsync(GetCurrentUserId(), studentId, status);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _routineService.GetByIdAsync(GetCurrentUserId(), id);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoutineDto request)
        {
            var result = await _routineService.UpdateAsync(GetCurrentUserId(), id, request);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> SetStatus(int id, [FromBody] SetRoutineStatusDto request)
        {
            var result = await _routineService.SetStatusAsync(GetCurrentUserId(), id, request.Status);
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
