using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var serviceResponse = await _lessonService.GetAllAsync(pagination);

            if (serviceResponse.IsSuccess && serviceResponse.Data != null)
            {
                var lessonDtoCollection = serviceResponse.Data.Data.Select(l => new LessonDto
                {
                    Id = l.Id,
                    ModuleId = l.ModuleId,
                    Title = l.Title,
                    Description = l.Description,
                    Type = l.Type,
                    DurationMinutes = l.DurationMinutes,
                    CreatedAt = l.CreatedAt
                });

                var apiResponse = new ApiResponse<IEnumerable<LessonDto>>
                {
                    Data = lessonDtoCollection,
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

            var serviceResponse = await _lessonService.GetByIdAsync(id);

            if (serviceResponse.IsSuccess)
            {
                var lessonDto = new LessonDto
                {
                    Id = serviceResponse.Data!.Id,
                    ModuleId = serviceResponse.Data!.ModuleId,
                    Title = serviceResponse.Data!.Title,
                    Description = serviceResponse.Data!.Description,
                    Type = serviceResponse.Data!.Type,
                    DurationMinutes = serviceResponse.Data!.DurationMinutes,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return Ok(lessonDto);
            }

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    var unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "404",
                        Message = "No se encontro una leccion asociada al Id proporcionado",
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
        public async Task<IActionResult> Add([FromBody] CreateLessonDto lessonDto)
        {
            var serviceResponse = await _lessonService.CreateAsync(lessonDto);

            if (serviceResponse.IsSuccess)
            {
                var newLessonDto = new LessonDto
                {
                    Id = serviceResponse.Data!.Id,
                    ModuleId = serviceResponse.Data!.ModuleId,
                    Title = serviceResponse.Data!.Title,
                    Description = serviceResponse.Data!.Description,
                    Type = serviceResponse.Data!.Type,
                    DurationMinutes = serviceResponse.Data!.DurationMinutes,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = newLessonDto.Id }, newLessonDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.ErrorValidation:
                    unSuccessfulResponse.Code = "400";
                    unSuccessfulResponse.Message = "El modulo proporcionado no existe";
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLessonDto lessonDto)
        {
            var serviceResponse = await _lessonService.UpdateAsync(id, lessonDto);

            if (serviceResponse.IsSuccess)
            {
                var updatedLessonDto = new LessonDto
                {
                    Id = serviceResponse.Data!.Id,
                    ModuleId = serviceResponse.Data!.ModuleId,
                    Title = serviceResponse.Data!.Title,
                    Description = serviceResponse.Data!.Description,
                    Type = serviceResponse.Data!.Type,
                    DurationMinutes = serviceResponse.Data!.DurationMinutes,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };
                return Ok(updatedLessonDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unSuccessfulResponse.Code = "404";
                    unSuccessfulResponse.Message = "No se encontro leccion con el Id proporcionado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Recurso no encontrado" };
                    return NotFound(unSuccessfulResponse);

                case MessageCodes.ErrorValidation:
                    unSuccessfulResponse.Code = "400";
                    unSuccessfulResponse.Message = "El modulo proporcionado no existe";
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