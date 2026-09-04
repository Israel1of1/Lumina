
using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;

namespace Business.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<ServiceResponse<PagedResponse<IEnumerable<Module>>>> GetAllAsync(PaginationParams pagination)
        {
            var result = await _moduleRepository.GetAllAsync(pagination);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<PagedResponse<IEnumerable<Module>>>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50170:
                    return new ServiceResponse<PagedResponse<IEnumerable<Module>>>
                    {
                        Data = null,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };

                default:
                    return new ServiceResponse<PagedResponse<IEnumerable<Module>>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<Module>> GetByIdAsync(int id)
        {
            var repoResponse = await _moduleRepository.GetByIdAsync(id);

            try
            {
                if (repoResponse.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Module>
                    {
                        Data = repoResponse.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = repoResponse.Message ?? "Operacion exitosa"
                    };
                }
                switch (repoResponse.OperationStatusCode)
                {
                    case 50171:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = repoResponse.Message ?? "No existe el modulo"
                        };

                    default:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = repoResponse.Message ?? "Ocurrio un error inesperado"
                        };
                }
            }
            catch (Exception)
            {
                return new ServiceResponse<Module>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = repoResponse.Message ?? "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<Module>> CreateAsync(CreateModuleDto newModule)
        {
            try
            {
                var module = new Module()
                {
                    SubjectId = newModule.SubjectId,
                    Name = newModule.Name,
                    Description = newModule.Description,
                    IconUrl = newModule.IconUrl
                };

                var result = await _moduleRepository.AddAsync(module);

                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Module>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Registro creado con exito"
                    };
                }

                switch (result.OperationStatusCode)
                {
                    case 50172:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorValidation,
                            Message = "No existe la materia (SubjectId) proporcionada"
                        };

                    default:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = result.Message ?? "Ocurrio un error inesperado"
                        };
                }
            }
            catch (Exception)
            {
                return new ServiceResponse<Module>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<Module>> UpdateAsync(int id, UpdateModuleDto module)
        {
            try
            {
                var dataModule = new Module()
                {
                    SubjectId = module.SubjectId,
                    Name = module.Name,
                    Description = module.Description,
                    IconUrl = module.IconUrl
                };

                var result = await _moduleRepository.UpdateAsync(id, dataModule);

                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Module>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Registro actualizado con exito"
                    };
                }

                switch (result.OperationStatusCode)
                {
                    case 50171:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = "No existe el modulo con el Id proporcionado"
                        };

                    case 50172:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorValidation,
                            Message = "No existe la materia (SubjectId) proporcionada"
                        };

                    default:
                        return new ServiceResponse<Module>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorDataBase,
                            Message = result.Message ?? "Ocurrio un error inesperado"
                        };
                }
            }
            catch (Exception)
            {
                return new ServiceResponse<Module>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error inesperado"
                };
            }
        }
    }
}