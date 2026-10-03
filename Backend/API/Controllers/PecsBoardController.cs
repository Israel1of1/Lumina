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
    [Route("api/pecs-boards")]
    [Authorize]
    public class PecsBoardController : ControllerBase
    {
        private readonly IPecsBoardService _boardService;

        public PecsBoardController(IPecsBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePecsBoardDto request)
        {
            var result = await _boardService.CreateAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        [HttpGet("by-student/{studentId:int}")]
        public async Task<IActionResult> GetByStudent(int studentId)
        {
            var result = await _boardService.GetByStudentAsync(GetCurrentUserId(), studentId);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePecsBoardDto request)
        {
            var result = await _boardService.UpdateAsync(GetCurrentUserId(), id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _boardService.DeleteAsync(GetCurrentUserId(), id);
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
