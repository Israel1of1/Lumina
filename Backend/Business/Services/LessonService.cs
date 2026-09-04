using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;

namespace Business.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public async Task<ServiceResponse<PagedResponse<IEnumerable<Lesson>>>> GetAllAsync(PaginationParams pagination)
        {
            var result = await _lessonRepository.GetAllAsync(pagination);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<PagedResponse<IEnumerable<Lesson>>>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50180:
                    return new ServiceResponse<PagedResponse<IEnumerable<Lesson>>>
                    {
                        Data = null,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };

                default:
                    return new ServiceResponse<PagedResponse<IEnumerable<Lesson>>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<Lesson>> GetByIdAsync(int id)
        {
            var repoResponse = await _lessonRepository.GetByIdAsync(id);

            try
            {
                if (repoResponse.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Lesson>
                    {
                        Data = repoResponse.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = repoResponse.Message ?? "Operacion exitosa"
                    };
                }
                switch (repoResponse.OperationStatusCode)
                {
                    case 50181:
                        return new ServiceResponse<Lesson>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = repoResponse.Message ?? "No existe la leccion"
                        };

                    default:
                        return new ServiceResponse<Lesson>
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
                return new ServiceResponse<Lesson>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = repoResponse.Message ?? "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<Lesson>> CreateAsync(CreateLessonDto newLesson)
        {
            try
            {
                var lesson = new Lesson()
                {
                    ModuleId = newLesson.ModuleId,
                    Title = newLesson.Title,
                    Description = newLesson.Description,
                    Type = newLesson.Type,
                    DurationMinutes = newLesson.DurationMinutes
                };

                var result = await _lessonRepository.AddAsync(lesson);

                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Lesson>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Registro creado con exito"
                    };
                }

                switch (result.OperationStatusCode)
                {
                    case 50182:
                        return new ServiceResponse<Lesson>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorValidation,
                            Message = "No existe el modulo (ModuleId) proporcionado"
                        };

                    default:
                        return new ServiceResponse<Lesson>
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
                return new ServiceResponse<Lesson>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<Lesson>> UpdateAsync(int id, UpdateLessonDto lesson)
        {
            try
            {
                var dataLesson = new Lesson()
                {
                    ModuleId = lesson.ModuleId,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    Type = lesson.Type,
                    DurationMinutes = lesson.DurationMinutes
                };

                var result = await _lessonRepository.UpdateAsync(id, dataLesson);

                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<Lesson>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Registro actualizado con exito"
                    };
                }

                switch (result.OperationStatusCode)
                {
                    case 50181:
                        return new ServiceResponse<Lesson>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = "No existe la leccion con el Id proporcionado"
                        };

                    case 50182:
                        return new ServiceResponse<Lesson>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorValidation,
                            Message = "No existe el modulo (ModuleId) proporcionado"
                        };

                    default:
                        return new ServiceResponse<Lesson>
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
                return new ServiceResponse<Lesson>
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