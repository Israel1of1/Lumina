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
    [Route("api/routine-logs")]
    [Authorize]
    public class RoutineLogController : ControllerBase
    {
        private readonly IRoutineLogService _logService;

        public RoutineLogController(IRoutineLogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoutineLogDto request)
        {
            var result = await _logService.CreateAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        [HttpGet("by-student/{studentId:int}")]
        public async Task<IActionResult> GetByStudent(int studentId, [FromQuery] DateTime? fromDate = null, [FromQuery] DateTime? toDate = null)
        {
            var result = await _logService.GetByStudentAsync(GetCurrentUserId(), studentId, fromDate, toDate);
            return MapResponse(result);
        }

        [HttpGet("by-detail/{routineDetailId:int}")]
        public async Task<IActionResult> GetByDetail(int routineDetailId)
        {
            var result = await _logService.GetByDetailAsync(GetCurrentUserId(), routineDetailId);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoutineLogDto request)
        {
            var result = await _logService.UpdateAsync(GetCurrentUserId(), id, request);
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
