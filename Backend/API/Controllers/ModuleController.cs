
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
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var serviceResponse = await _moduleService.GetAllAsync(pagination);

            if (serviceResponse.IsSuccess && serviceResponse.Data != null)
            {
                var moduleDtoCollection = serviceResponse.Data.Data.Select(m => new ModuleDto
                {
                    Id = m.Id,
                    SubjectId = m.SubjectId,
                    Name = m.Name,
                    Description = m.Description,
                    IconUrl = m.IconUrl,
                    CreatedAt = m.CreatedAt
                });

                var apiResponse = new ApiResponse<IEnumerable<ModuleDto>>
                {
                    Data = moduleDtoCollection,
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

            var serviceResponse = await _moduleService.GetByIdAsync(id);

            if (serviceResponse.IsSuccess)
            {
                var moduleDto = new ModuleDto
                {
                    Id = serviceResponse.Data!.Id,
                    SubjectId = serviceResponse.Data!.SubjectId,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    IconUrl = serviceResponse.Data!.IconUrl,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return Ok(moduleDto);
            }

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    var unsuccessfulResponse = new UnsuccessfulResponseDto
                    {
                        Code = "404",
                        Message = "No se encontro un modulo asociado al Id proporcionado",
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
        public async Task<IActionResult> Add([FromBody] CreateModuleDto moduleDto)
        {
            var serviceResponse = await _moduleService.CreateAsync(moduleDto);

            if (serviceResponse.IsSuccess)
            {
                var newModuleDto = new ModuleDto
                {
                    Id = serviceResponse.Data!.Id,
                    SubjectId = serviceResponse.Data!.SubjectId,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    IconUrl = serviceResponse.Data!.IconUrl,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = newModuleDto.Id }, newModuleDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.ErrorValidation:
                    unSuccessfulResponse.Code = "400";
                    unSuccessfulResponse.Message = "La materia proporcionada no existe";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return BadRequest(unSuccessfulResponse);

                default:
                    unSuccessfulResponse.Code = "500";
                    unSuccessfulResponse.Message = "Ocurrio un error inesperado";
                    unSuccessfulResponse.Details = new { info = "Error interno inesperado" };
                    return StatusCode(500, unSuccessfulResponse);
            }
        }

        //[Authorize(Roles = "INSTITUTION, TEACHER")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateModuleDto moduleDto)
        {
            var serviceResponse = await _moduleService.UpdateAsync(id, moduleDto);

            if (serviceResponse.IsSuccess)
            {
                var updatedModuleDto = new ModuleDto
                {
                    Id = serviceResponse.Data!.Id,
                    SubjectId = serviceResponse.Data!.SubjectId,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    IconUrl = serviceResponse.Data!.IconUrl,
                    CreatedAt = serviceResponse.Data!.CreatedAt
                };
                return Ok(updatedModuleDto);
            }

            var unSuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unSuccessfulResponse.Code = "404";
                    unSuccessfulResponse.Message = "No se encontro modulo con el Id proporcionado";
                    unSuccessfulResponse.Details = new { info = serviceResponse.Message ?? "Recurso no encontrado" };
                    return NotFound(unSuccessfulResponse);

                case MessageCodes.ErrorValidation:
                    unSuccessfulResponse.Code = "400";
                    unSuccessfulResponse.Message = "La materia proporcionada no existe";
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