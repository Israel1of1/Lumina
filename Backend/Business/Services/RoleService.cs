using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;

using DataAccess.Interfaces;

namespace Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<ServiceResponse<IEnumerable<Role>>> GetAllAsync()
        {
            var result = await _roleRepository.GetAllAsync();

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<IEnumerable<Role>>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operación exitosa"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50050:
                    return new ServiceResponse<IEnumerable<Role>>
                    {
                        Data = null,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };

                default:
                    return new ServiceResponse<IEnumerable<Role>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<Role>> GetByIdAsync(int id)
        {
            var result = await _roleRepository.GetByIdAsync(id);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<Role>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operación exitosa"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50050:
                    return new ServiceResponse<Role>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = "No se encontró un rol con el Id proporcionado"
                    };

                default:
                    return new ServiceResponse<Role>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<Role>> CreateAsync(CreateRoleDto newRole)
        {
            var role = new Role
            {
                Name = newRole.Name,
                Description = newRole.Description,
                IsActive = true
            };

            var result = await _roleRepository.AddAsync(role);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<Role>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Rol creado exitosamente"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50020:
                    return new ServiceResponse<Role>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = "Ya existe un rol con ese nombre"
                    };

                default:
                    return new ServiceResponse<Role>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<Role>> UpdateAsync(int id, UpdateRoleDto role)
        {
            var roleToUpdate = new Role
            {
                Name = role.Name,
                Description = role.Description,
                IsActive = role.IsActive
            };

            var result = await _roleRepository.UpdateAsync(id, roleToUpdate);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<Role>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Rol actualizado exitosamente"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50050:
                    return new ServiceResponse<Role>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = "No se encontró un rol con el Id proporcionado"
                    };

                default:
                    return new ServiceResponse<Role>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }
    }
}