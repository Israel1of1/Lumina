using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonStepsController : ControllerBase
    {
        private readonly ILessonStepService _lessonStepService;

        public LessonStepsController(ILessonStepService lessonStepService)
        {
            _lessonStepService = lessonStepService;
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var serviceResponse = await _lessonStepService.GetAllAsync(pagination);

            if (serviceResponse.IsSuccess && serviceResponse.Data != null)
            {
                var lessonStepDtoCollection = serviceResponse.Data.Data.Select(ls => new LessonStepDto
                {
                    Id = ls.Id,
                    LessonId = ls.LessonId,
                    StepNumber = ls.StepNumber,
                    Title = ls.Title,
                    Description = ls.Description,
                    ContentType = ls.ContentType,
                    ContentUrl = ls.ContentUrl,
                    IsActive = ls.IsActive,
                    CreatedAt = ls.CreatedAt
                });

                var apiResponse = new ApiResponse<IEnumerable<LessonStepDto>>
                {
                    Data = lessonStepDtoCollection,
                    Meta = new
                    {
                        totalRecords = serviceResponse.Data.TotalRecords,
                        totalPages = serviceResponse.Data.TotalPages,
                        pageNumber = serviceResponse.Data.PageNumber,
                        pageSize = serviceResponse.Data.PageSize,
                        message = serviceResponse.Message
                    }
                };

                return Ok(apiResponse);
            }

            var unsuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NoData:
                    unsuccessfulResponse.Code = "200";
                    unsuccessfulResponse.Message = "No se encontraron registros";
                    unsuccessfulResponse.Details = new { info = "Temporalmente no hay registros en la BD" };
                    return Ok(unsuccessfulResponse);

                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unsuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Error interno en la aplicacion" };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                var response = new UnsuccessfulResponseDto()
                {
                    Code = "400",
                    Message = "Id proporcionado debe ser mayor a 0",
                    Details = new { info = "Error en el formato del valor enviado" }
                };
                return BadRequest(response);
            }

            var serviceResponse = await _lessonStepService.GetByIdAsync(id);

            if (serviceResponse.IsSuccess)
            {
                var lessonStepDto = new LessonStepDto
                {
                    Id = serviceResponse.Data!.Id,
                    LessonId = serviceResponse.Data!.LessonId,
                    StepNumber = serviceResponse.Data!.StepNumber,
                    Title = serviceResponse.Data!.Title,
                    Description = serviceResponse.Data!.Description,
                    ContentType = serviceResponse.Data!.ContentType,
                    ContentUrl = serviceResponse.Data!.ContentUrl,
                    IsActive = serviceResponse.Data!.IsActive,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return Ok(lessonStepDto);
            }

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    var unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "404",
                        Message = "No se encontro un paso de leccion asociado al Id proporcionado",
                        Details = new { info = serviceResponse.Message ?? "No se encontró el recurso solicitado" }
                    };
                    return NotFound(unsuccessfulResponse);

                default:
                    unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "500",
                        Message = "Ocurrió un error",
                        Details = new { info = serviceResponse.Message ?? "Error interno no esperado" }
                    };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLessonStepDto lessonStepDto)
        {
            var serviceResponse = await _lessonStepService.CreateAsync(lessonStepDto);

            if (serviceResponse.IsSuccess)
            {
                var newLessonStepDto = new LessonStepDto
                {
                    Id = serviceResponse.Data!.Id,
                    LessonId = serviceResponse.Data!.LessonId,
                    StepNumber = serviceResponse.Data!.StepNumber,
                    Title = serviceResponse.Data!.Title,
                    Description = serviceResponse.Data!.Description,
                    ContentType = serviceResponse.Data!.ContentType,
                    ContentUrl = serviceResponse.Data!.ContentUrl,
                    IsActive = serviceResponse.Data!.IsActive,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = newLessonStepDto.Id }, newLessonStepDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.ErrorValidation:
                    unSuccessfulResponse.Code = "400";
                    unSuccessfulResponse.Message = "La leccion proporcionada no existe";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return BadRequest(unSuccessfulResponse);

                default:
                    unSuccessfulResponse.Code = "500";
                    unSuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Error interno inesperado" };
                    return StatusCode(500, unSuccessfulResponse);
            }
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLessonStepDto lessonStepDto)
        {
            var serviceResponse = await _lessonStepService.UpdateAsync(id, lessonStepDto);

            if (serviceResponse.IsSuccess)
            {
                var updatedLessonStepDto = new LessonStepDto
                {
                    Id = serviceResponse.Data!.Id,
                    LessonId = serviceResponse.Data!.LessonId,
                    StepNumber = serviceResponse.Data!.StepNumber,
                    Title = serviceResponse.Data!.Title,
                    Description = serviceResponse.Data!.Description,
                    ContentType = serviceResponse.Data!.ContentType,
                    ContentUrl = serviceResponse.Data!.ContentUrl,
                    IsActive = serviceResponse.Data!.IsActive,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };
                return Ok(updatedLessonStepDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unSuccessfulResponse.Code = "404";
                    unSuccessfulResponse.Message = "No se encontro paso de leccion con el Id proporcionado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Recurso no encontrado" };
                    return NotFound(unSuccessfulResponse);

                case MessageCodes.ErrorValidation:
                    unSuccessfulResponse.Code = "400";
                    unSuccessfulResponse.Message = "La leccion proporcionada no existe";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return BadRequest(unSuccessfulResponse);

                default:
                    unSuccessfulResponse.Code = "500";
                    unSuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Error interno inesperado" };
                    return StatusCode(500, unSuccessfulResponse);
            }
        }
    }
}