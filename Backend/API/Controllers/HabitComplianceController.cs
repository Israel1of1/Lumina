using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/habit-compliances")]
    [Authorize]
    public class HabitComplianceController : ControllerBase
    {
        private readonly IHabitComplianceService _habitComplianceService;

        public HabitComplianceController(IHabitComplianceService habitComplianceService)
        {
            _habitComplianceService = habitComplianceService;
        }

        [HttpGet("by-habit/{habitId:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> GetByHabit(int habitId)
        {
            var result = await _habitComplianceService.GetByHabitAsync(habitId);
            return MapResponse(result);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _habitComplianceService.GetByIdAsync(id);
            return MapResponse(result);
        }

        [HttpPost]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> Create([FromBody] CreateHabitComplianceDto request)
        {
            var result = await _habitComplianceService.CreateAsync(request);
            return MapResponse(result);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateHabitComplianceDto request)
        {
            var result = await _habitComplianceService.UpdateAsync(id, request);
            return MapResponse(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "INSTITUTION,INSTITUCION,TEACHER,DOCENTE,GUARDIAN,TUTOR")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _habitComplianceService.DeleteAsync(id);
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