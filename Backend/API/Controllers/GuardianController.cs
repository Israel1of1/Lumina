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
    [Route("api/guardians")]
    [Authorize]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianService _guardianService;

        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }

        [HttpGet("me")]
        [Authorize(Roles = "TUTOR")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _guardianService.GetMyProfileAsync(GetCurrentUserId());
            return MapResponse(result);
        }

        [HttpPatch("me")]
        [Authorize(Roles = "TUTOR")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateGuardianProfileDto request)
        {
            var result = await _guardianService.UpdateMyProfileAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }

        /// <summary>Lista todos los tutores. Uso institucional.</summary>
        [HttpGet]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? status = null)
        {
            var result = await _guardianService.GetAllAsync(pageNumber, pageSize, status);
            return MapResponse(result);
        }

        /// <summary>Da de baja a un tutor (cierra tambien su acceso al login). Uso institucional.</summary>
        [HttpPatch("{id:int}/deactivate")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Deactivate(int id, [FromBody] DeactivateRequestDto request)
        {
            var result = await _guardianService.DeactivateAsync(id, request.Reason);
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
