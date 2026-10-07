using Business.DTOs;
using Business.Interfaces;
using Business.Services;
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

        [HttpPost]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> Create([FromBody] CreateGuardianDto request)
        {
            var result = await _guardianService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpGet("me")]
        [Authorize(Roles = "GUARDIAN")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _guardianService.GetMyProfileAsync(GetCurrentUserId());
            return MapResponse(result);
        }

        [HttpPut("me")]
        [Authorize(Roles = "GUARDIAN")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateGuardianProfileDto request)
        {
            var result = await _guardianService.UpdateMyProfileAsync(GetCurrentUserId(), request);
            return MapResponse(result);
        }


        [HttpPatch("me")]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> PatchProfile(int Id,[FromBody] PatchGuardianProfileDto request)
        {
            var result = await _guardianService.PatchMyProfileAsync(Id, request);
            return MapResponse(result);
        }


        //Lista todos los tutores 
        [HttpGet]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? status = null)
        {
            var result = await _guardianService.GetAllAsync(pageNumber, pageSize, status);
            return MapResponse(result);
        }

        // de baja a un tutor cierra tambien su acceso al login  
        [HttpPatch("{id:int}/deactivate")]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> Deactivate(int id, [FromBody] DeactivateRequestDto request)
        {
            var result = await _guardianService.DeactivateAsync(id, request.Reason);
            return MapResponse(result);
        }



        [HttpPatch("{id:int}/reactivate")]
        [Authorize(Roles = "INSTITUTION")]
        public async Task<IActionResult> Reactivate(int id)
        {
            var result = await _guardianService.ReactivateAsync(id);
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
