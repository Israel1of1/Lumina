
using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<ServiceResponse<PagedResponse<IEnumerable<Subject>>>> GetAllAsync(PaginationParams pagination)
        {
            var result = await _subjectRepository.GetAllAsync(pagination);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<PagedResponse<IEnumerable<Subject>>>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50137:
                    return new ServiceResponse<PagedResponse<IEnumerable<Subject>>>
                    {
                        Data = null,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };

                default:
                    return new ServiceResponse<PagedResponse<IEnumerable<Subject>>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<Subject>> GetByIdAsync(int id)
        {
            var repoResponse = await _subjectRepository.GetByIdAsync(id);

            try
            {
                if (repoResponse.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Subject>
                    {
                        Data = repoResponse.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = repoResponse.Message ?? "Operacion exitosa"
                    };
                }
                switch (repoResponse.OperationStatusCode)
                {
                    case 50150:
                        return new ServiceResponse<Subject>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = repoResponse.Message ?? "No existe la materia"
                        };

                    default:
                        return new ServiceResponse<Subject>
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
                return new ServiceResponse<Subject>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = repoResponse.Message ?? "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<Subject>> GetByNameAsync(string name)
        {
            var result = await _subjectRepository.GetByNameAsync(name);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<Subject>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };
            }

            var messageCode = MessageCodes.ErrorDataBase;
            var message = "Error en la base de datos al obtener la materia.";

            switch (result.OperationStatusCode)
            {
                case 50151:
                    messageCode = MessageCodes.NotFound;
                    message = "No existe la materia";
                    break;
            }

            return new ServiceResponse<Subject>
            {
                Data = null,
                IsSuccess = false,
                MessageCodes = messageCode,
                Message = message
            };
        }

        public async Task<ServiceResponse<Subject>> CreateAsync(CreateSubjectDto newSubject)
        {
            try
            {
                var existingSubject = await _subjectRepository.GetByNameAsync(newSubject.Name);

                if (existingSubject.Data!.Id != 0 && !string.IsNullOrEmpty(existingSubject.Data.Name))
                {
                    return new ServiceResponse<Subject>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = "Existe una materia con el nombre proporcionado"
                    };
                }

                var subject = new Subject()
                {
                    Name = newSubject.Name,
                    Description = newSubject.Description,
                    Color = newSubject.Color,
                    Icon = newSubject.Icon
                };

                var result = await _subjectRepository.AddAsync(subject);

                return new ServiceResponse<Subject>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Registro creado con exito"
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<Subject>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<Subject>> UpdateAsync(int id, UpdateSubjectDto subject)
        {
            try
            {
                var existingIdSubject = await _subjectRepository.GetByIdAsync(id);

                if (existingIdSubject.Data!.Id == 0 && string.IsNullOrEmpty(existingIdSubject.Data.Name))
                {
                    return new ServiceResponse<Subject>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.NotFound,
                        Message = "No existe la materia con el Id proporcionado"
                    };
                }

                var existingNameSubject = await _subjectRepository.GetByNameAsync(subject.Name);
                if (existingNameSubject.Data!.Name != null && existingNameSubject.Data.Id != id)
                {
                    return new ServiceResponse<Subject>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict,
                        Message = "Ya existe una materia con el nombre proporcionado. No se debe duplicar el nombre"
                    };
                }

                var dataSubject = new Subject()
                {
                    Name = subject.Name,
                    Description = subject.Description,
                    Color = subject.Color,
                    Icon = subject.Icon
                };

                var result = await _subjectRepository.UpdateAsync(id, dataSubject);

                return new ServiceResponse<Subject>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Registro actualizado con exito"
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<Subject>
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