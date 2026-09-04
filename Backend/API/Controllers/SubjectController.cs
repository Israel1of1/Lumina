
using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var serviceResponse = await _subjectService.GetAllAsync(pagination);

            if (serviceResponse.IsSuccess && serviceResponse.Data != null)
            {
                var subjectDtoCollection = serviceResponse.Data.Data.Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Color = s.Color,
                    Icon = s.Icon,
                    CreatedAt = s.CreatedAt
                });

                var apiResponse = new ApiResponse<IEnumerable<SubjectDto>>
                {
                    Data = subjectDtoCollection,
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
                    unsuccessfulResponse.Details = new { info = "Error interno en la aplicacion" };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        [Authorize(Roles = "INSTITUTION, TEACHER")]
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

            var serviceResponse = await _subjectService.GetByIdAsync(id);

            if (serviceResponse.IsSuccess)
            {
                var subjectDto = new SubjectDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    Color = serviceResponse.Data!.Color,
                    Icon = serviceResponse.Data!.Icon,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return Ok(subjectDto);
            }

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    var unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "404",
                        Message = "No se encontro una materia asociada al Id proporcionado",
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

        [Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpGet("byname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            if (string.IsNullOrWhiteSpace(name))
            {
                unSuccessfulResponse.Code = "400";
                unSuccessfulResponse.Message = "El dato proporcionado no es válido";
                unSuccessfulResponse.Details = new { Error = "El Name no puede ser nulo o vacío" };
                return BadRequest(unSuccessfulResponse);
            }

            var serviceResponse = await _subjectService.GetByNameAsync(name);

            if (serviceResponse.IsSuccess)
            {
                var subjectDto = new SubjectDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    Color = serviceResponse.Data!.Color,
                    Icon = serviceResponse.Data!.Icon,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return Ok(subjectDto);
            }

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unSuccessfulResponse.Code = "404";
                    unSuccessfulResponse.Message = "No se encontró la materia asociada al valor de Name proporcionado";
                    unSuccessfulResponse.Details = new { Error = "No hay registros asociados al valor de Name proporcionado" };
                    return NotFound(unSuccessfulResponse);

                default:
                    unSuccessfulResponse.Code = "500";
                    unSuccessfulResponse.Message = serviceResponse.Message ?? "Ocurrió un error inesperado";
                    return StatusCode(500, unSuccessfulResponse);
            }
        }

        [Authorize(Roles = "INSTITUTION")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSubjectDto subjectDto)
        {
            var serviceResponse = await _subjectService.CreateAsync(subjectDto);

            if (serviceResponse.IsSuccess)
            {
                var newSubjectDto = new SubjectDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    Color = serviceResponse.Data!.Color,
                    Icon = serviceResponse.Data!.Icon,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = newSubjectDto.Id }, newSubjectDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.Conflict:
                    unSuccessfulResponse.Code = "409";
                    unSuccessfulResponse.Message = "El nombre de la materia ya existe";
                    unSuccessfulResponse.Details = new { info = "No se puede duplicar el nombre de una materia" };
                    return Conflict(unSuccessfulResponse);

                default:
                    unSuccessfulResponse.Code = "500";
                    unSuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unSuccessfulResponse.Details = new { info = "Error interno inesperado" };
                    return StatusCode(500, unSuccessfulResponse);
            }
        }

        [Authorize(Roles = "INSTITUTION")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSubjectDto subjectDto)
        {
            var serviceResponse = await _subjectService.UpdateAsync(id, subjectDto);

            if (serviceResponse.IsSuccess)
            {
                var updatedSubjectDto = new SubjectDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    Color = serviceResponse.Data!.Color,
                    Icon = serviceResponse.Data!.Icon,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };
                return Ok(updatedSubjectDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unSuccessfulResponse.Code = "404";
                    unSuccessfulResponse.Message = "No se encontro materia con el Id proporcionado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Recurso no encontrado" };
                    return NotFound(unSuccessfulResponse);

                case MessageCodes.Conflict:
                    unSuccessfulResponse.Code = "409";
                    unSuccessfulResponse.Message = "El registro no pudo guardarse por un conflicto";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Hubo conflicto con el nombre, no debe duplicarse" };
                    return Conflict(unSuccessfulResponse);

                default:
                    unSuccessfulResponse.Code = "500";
                    unSuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Error interno inesperado" };
                    return StatusCode(500, unSuccessfulResponse);
            }
        }
    }
}