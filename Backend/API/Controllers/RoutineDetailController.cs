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
    [Route("api/routine-details")]
    [Authorize]
    public class RoutineDetailController : ControllerBase
    {
        private readonly IRoutineDetailService _detailService;

        public RoutineDetailController(IRoutineDetailService detailService)
        {
            _detailService = detailService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoutineDetailDto request)
        {
            var result = await _detailService.CreateAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        [HttpGet("by-routine/{routineId:int}")]
        public async Task<IActionResult> GetByRoutine(int routineId)
        {
            var result = await _detailService.GetByRoutineAsync(GetCurrentUserId(), routineId);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoutineDetailDto request)
        {
            var result = await _detailService.UpdateAsync(GetCurrentUserId(), id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _detailService.DeleteAsync(GetCurrentUserId(), id);
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
