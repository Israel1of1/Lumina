using Business.DTOs;
using Business.Interfaces;
using Core.Common;

using Microsoft.AspNetCore.Mvc;

namespace Lumina.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResponse = await _roleService.GetAllAsync();

            if (serviceResponse.IsSuccess && serviceResponse.Data != null)
            {
                var roleDtoCollection = serviceResponse.Data.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    IsActive = r.IsActive,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                });

                var apiResponse = new ApiResponse<IEnumerable<RoleDto>>
                {
                    Data = roleDtoCollection,
                    Meta = new { message = serviceResponse.Message }
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
                    unsuccessfulResponse.Message = "Ocurrió un error inesperado";
                    unsuccessfulResponse.Details = new { info = "Error interno en la aplicación" };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                var badRequest = new UnsuccessfulResponseDto
                {
                    Code = "400",
                    Message = "Id proporcionado debe ser mayor a 0",
                    Details = new { info = "Error en el formato del valor enviado" }
                };
                return BadRequest(badRequest);
            }

            var serviceResponse = await _roleService.GetByIdAsync(id);

            if (serviceResponse.IsSuccess)
            {
                var roleDto = new RoleDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    IsActive = serviceResponse.Data!.IsActive,
                    CreatedAt = serviceResponse.Data!.CreatedAt,
                    UpdatedAt = serviceResponse.Data!.UpdatedAt
                };
                return Ok(roleDto);
            }

            var unsuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unsuccessfulResponse.Code = "404";
                    unsuccessfulResponse.Message = "No se encontró un rol con el Id proporcionado";
                    unsuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return NotFound(unsuccessfulResponse);

                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrió un error";
                    unsuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateRoleDto roleDto)
        {
            var serviceResponse = await _roleService.CreateAsync(roleDto);

            if (serviceResponse.IsSuccess)
            {
                var newRoleDto = new RoleDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    IsActive = serviceResponse.Data!.IsActive,
                    CreatedAt = serviceResponse.Data!.CreatedAt,
                    UpdatedAt = serviceResponse.Data!.UpdatedAt
                };

                return CreatedAtAction(nameof(GetById), new { id = newRoleDto.Id }, newRoleDto);
            }

            var unsuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.Conflict:
                    unsuccessfulResponse.Code = "409";
                    unsuccessfulResponse.Message = "El nombre del rol ya existe";
                    unsuccessfulResponse.Details = new { info = "No se puede duplicar un nombre de rol" };
                    return Conflict(unsuccessfulResponse);

                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrió un error inesperado";
                    unsuccessfulResponse.Details = new { info = "Error interno inesperado" };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleDto roleDto)
        {
            var serviceResponse = await _roleService.UpdateAsync(id, roleDto);

            if (serviceResponse.IsSuccess)
            {
                var updatedRoleDto = new RoleDto
                {
                    Id = serviceResponse.Data!.Id,
                    Name = serviceResponse.Data!.Name,
                    Description = serviceResponse.Data!.Description,
                    IsActive = serviceResponse.Data!.IsActive,
                    CreatedAt = serviceResponse.Data!.CreatedAt,
                    UpdatedAt = serviceResponse.Data!.UpdatedAt
                };
                return Ok(updatedRoleDto);
            }

            var unsuccessfulResponse = new UnsuccessfulResponseDto();

            switch (serviceResponse.MessageCodes)
            {
                case MessageCodes.NotFound:
                    unsuccessfulResponse.Code = "404";
                    unsuccessfulResponse.Message = "No se encontró rol con el Id proporcionado";
                    unsuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return NotFound(unsuccessfulResponse);

                default:
                    unsuccessfulResponse.Code = "500";
                    unsuccessfulResponse.Message = "Ocurrió un error inesperado";
                    unsuccessfulResponse.Details = new { info = serviceResponse.Message };
                    return StatusCode(500, unsuccessfulResponse);
            }
        }
    }
}
