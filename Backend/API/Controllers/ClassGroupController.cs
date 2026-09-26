using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/class-groups")]
    [Authorize]
    public class ClassGroupController : ControllerBase
    {
        private readonly IClassGroupService _classGroupService;

        public ClassGroupController(IClassGroupService classGroupService)
        {
            _classGroupService = classGroupService;
        }

        [HttpGet]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] bool? isActive = null)
        {
            var result = await _classGroupService.GetAllAsync(pageNumber, pageSize, isActive);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _classGroupService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Create([FromBody] CreateClassGroupDto request)
        {
            var result = await _classGroupService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateClassGroupDto request)
        {
            var result = await _classGroupService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpPatch("{id:int}/active")]
        [Authorize(Roles = "INSTITUCION")]
        public async Task<IActionResult> SetActive(int id, [FromBody] SetActiveDto request)
        {
            var result = await _classGroupService.SetActiveAsync(id, request.IsActive);
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
