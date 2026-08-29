using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;

namespace Business.Services
{
    public class LessonStepService : ILessonStepService
    {
        private readonly ILessonStepRepository _lessonStepRepository;

        public LessonStepService(ILessonStepRepository lessonStepRepository)
        {
            _lessonStepRepository = lessonStepRepository;
        }

        public async Task<ServiceResponse<PagedResponse<IEnumerable<LessonStep>>>> GetAllAsync(PaginationParams pagination)
        {
            var result = await _lessonStepRepository.GetAllAsync(pagination);

            if (result.OperationStatusCode == 0)
            {
                return new ServiceResponse<PagedResponse<IEnumerable<LessonStep>>>
                {
                    Data = result.Data,
                    IsSuccess = true,
                    MessageCodes = MessageCodes.Success,
                    Message = "Operacion exitosa"
                };
            }

            switch (result.OperationStatusCode)
            {
                case 50190:
                    return new ServiceResponse<PagedResponse<IEnumerable<LessonStep>>>
                    {
                        Data = null,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.NoData,
                        Message = "No se encontraron registros"
                    };

                default:
                    return new ServiceResponse<PagedResponse<IEnumerable<LessonStep>>>
                    {
                        Data = null,
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase,
                        Message = result.Message ?? "Ocurrió un error inesperado"
                    };
            }
        }

        public async Task<ServiceResponse<LessonStep>> GetByIdAsync(int id)
        {
            var repoResponse = await _lessonStepRepository.GetByIdAsync(id);

            try
            {
                if (repoResponse.OperationStatusCode == 0)
                {
                    return new ServiceResponse<LessonStep>
                    {
                        Data = repoResponse.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = repoResponse.Message ?? "Operacion exitosa"
                    };
                }
                switch (repoResponse.OperationStatusCode)
                {
                    case 50191:
                        return new ServiceResponse<LessonStep>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = repoResponse.Message ?? "No existe el paso de leccion"
                        };

                    default:
                        return new ServiceResponse<LessonStep>
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
                return new ServiceResponse<LessonStep>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = repoResponse.Message ?? "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<LessonStep>> CreateAsync(CreateLessonStepDto newLessonStep)
        {
            try
            {
                var lessonStep = new LessonStep()
                {
                    LessonId = newLessonStep.LessonId,
                    StepNumber = newLessonStep.StepNumber,
                    Title = newLessonStep.Title,
                    Description = newLessonStep.Description,
                    ContentType = newLessonStep.ContentType,
                    ContentUrl = newLessonStep.ContentUrl,
                    IsActive = newLessonStep.IsActive
                };

                var result = await _lessonStepRepository.AddAsync(lessonStep);

                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<LessonStep>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Registro creado con exito"
                    };
                }

                switch (result.OperationStatusCode)
                {
                    case 50192:
                        return new ServiceResponse<LessonStep>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorValidation,
                            Message = "No existe la leccion (LessonId) proporcionada"
                        };

                    default:
                        return new ServiceResponse<LessonStep>
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
                return new ServiceResponse<LessonStep>
                {
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase,
                    Message = "Ocurrio un error inesperado"
                };
            }
        }

        public async Task<ServiceResponse<LessonStep>> UpdateAsync(int id, UpdateLessonStepDto lessonStep)
        {
            try
            {
                var dataLessonStep = new LessonStep()
                {
                    LessonId = lessonStep.LessonId,
                    StepNumber = lessonStep.StepNumber,
                    Title = lessonStep.Title,
                    Description = lessonStep.Description,
                    ContentType = lessonStep.ContentType,
                    ContentUrl = lessonStep.ContentUrl,
                    IsActive = lessonStep.IsActive
                };

                var result = await _lessonStepRepository.UpdateAsync(id, dataLessonStep);

                if (result.OperationStatusCode == 0)
                {
                    return new ServiceResponse<LessonStep>
                    {
                        Data = result.Data,
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success,
                        Message = "Registro actualizado con exito"
                    };
                }

                switch (result.OperationStatusCode)
                {
                    case 50191:
                        return new ServiceResponse<LessonStep>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.NotFound,
                            Message = "No existe el paso de leccion con el Id proporcionado"
                        };

                    case 50192:
                        return new ServiceResponse<LessonStep>
                        {
                            Data = null,
                            IsSuccess = false,
                            MessageCodes = MessageCodes.ErrorValidation,
                            Message = "No existe la leccion (LessonId) proporcionada"
                        };

                    default:
                        return new ServiceResponse<LessonStep>
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
                return new ServiceResponse<LessonStep>
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