using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Core.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class LearningContentService : ILearningContentService
    {
        private const int MaxPageSize = 100;
        private static readonly HashSet<string> ValidTypes = new() { "EXCEPTION", "ROUTINE", "DICTIONARY" };

        private readonly ILearningContentRepository _contentRepository;

        public LearningContentService(ILearningContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public async Task<ServiceResponse<PagedResultDto<LearningContentDto>>> GetAllAsync(LearningContentFilterDto request)
        {
            var type = NormalizeType(request.Type);
            if (type is not null && !ValidTypes.Contains(type))
                return Fail<PagedResultDto<LearningContentDto>>(MessageCodes.ErrorValidation, "Tipo invalido. Usa EXCEPTION, ROUTINE o DICTIONARY.");

            var filter = new LearningContentFilter
            {
                PageNumber = Math.Max(1, request.PageNumber),
                PageSize = Math.Clamp(request.PageSize, 1, MaxPageSize),
                LessonId = request.LessonId,
                SubjectId = request.SubjectId,
                Type = type,
                Level = string.IsNullOrWhiteSpace(request.Level) ? null : request.Level.Trim(),
                Search = string.IsNullOrWhiteSpace(request.Search) ? null : request.Search.Trim(),
                IsActive = request.IsActive
            };

            var repoResponse = await _contentRepository.GetAllAsync(filter);

            if (repoResponse.OperationStatusCode != 0)
                return Fail<PagedResultDto<LearningContentDto>>(MessageCodes.ErrorDataBase, "Ocurrio un error al consultar los contenidos.");

            var (items, totalRecords) = repoResponse.Data;

            return new ServiceResponse<PagedResultDto<LearningContentDto>>
            {
                Data = new PagedResultDto<LearningContentDto>
                {
                    Items = items.Select(MapDto).ToList(),
                    TotalRecords = totalRecords,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize
                },
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Contenidos obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<LearningContentDto>> GetByIdAsync(int id, bool includeInactive)
        {
            var repoResponse = await _contentRepository.GetByIdAsync(id);

            // Quien no puede ver inactivos como Tutor recibe no encontrado, sin revelar que existe
            if (repoResponse.OperationStatusCode == 0 && !includeInactive && !repoResponse.Data!.IsActive)
                return Fail<LearningContentDto>(MessageCodes.NotFound, "No se encontro el contenido indicado.");

            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultado");
        }

        public async Task<ServiceResponse<LearningContentDto>> CreateAsync(CreateLearningContentDto request)
        {
            var type = NormalizeType(request.Type);
            if (type is null || !ValidTypes.Contains(type))
                return Fail<LearningContentDto>(MessageCodes.ErrorValidation, "Tipo invalido. Usa EXCEPTION, ROUTINE o DICTIONARY.");

            var content = new LearningContent
            {
                LessonId = request.LessonId,
                Title = request.Title.Trim(),
                Description = request.Description,
                Type = type,
                SubjectId = request.SubjectId,
                Level = request.Level?.Trim()
            };

            var repoResponse = await _contentRepository.CreateAsync(content);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<LearningContentDto>> UpdateAsync(int id, UpdateLearningContentDto request)
        {
            var type = NormalizeType(request.Type);
            if (type is null || !ValidTypes.Contains(type))
                return Fail<LearningContentDto>(MessageCodes.ErrorValidation, "Tipo invalido. Usa EXCEPTION, ROUTINE o DICTIONARY.");

            var content = new LearningContent
            {
                LessonId = request.LessonId,
                Title = request.Title.Trim(),
                Description = request.Description,
                Type = type,
                SubjectId = request.SubjectId,
                Level = request.Level?.Trim()
            };

            var repoResponse = await _contentRepository.UpdateAsync(id, content);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<LearningContentDto>> SetActiveAsync(int id, bool isActive)
        {
            var repoResponse = await _contentRepository.SetActiveAsync(id, isActive);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, isActive ? "activado" : "desactivado");
        }

        private static string? NormalizeType(string? type)
            => string.IsNullOrWhiteSpace(type) ? null : type.Trim().ToUpperInvariant();

        private static LearningContentDto MapDto(LearningContent c) => new()
        {
            Id = c.Id,
            LessonId = c.LessonId,
            LessonTitle = c.LessonTitle,
            Title = c.Title,
            Description = c.Description,
            Type = c.Type,
            SubjectId = c.SubjectId,
            SubjectName = c.SubjectName,
            Level = c.Level,
            IsActive = c.IsActive,
            Keywords = c.Keywords,
            CreatedAt = c.CreatedAt
        };

        private static ServiceResponse<LearningContentDto> MapResponse(int statusCode, LearningContent? data, string action)
        {
            return statusCode switch
            {
                0 => new ServiceResponse<LearningContentDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Contenido {action} correctamente." },
                50200 => Fail<LearningContentDto>(MessageCodes.NotFound, "No se encontro el contenido indicado."),
                50201 => Fail<LearningContentDto>(MessageCodes.NotFound, "La clase (lesson) indicada no existe."),
                50202 => Fail<LearningContentDto>(MessageCodes.NotFound, "La materia indicada no existe."),
                50203 => Fail<LearningContentDto>(MessageCodes.ErrorValidation, "Tipo invalido. Usa EXCEPTION, ROUTINE o DICTIONARY."),
                _ => Fail<LearningContentDto>(MessageCodes.ErrorDataBase, "Ocurrio un error inesperado.")
            };
        }

        private static ServiceResponse<T> Fail<T>(MessageCodes code, string message) => new()
        {
            Data = default,
            IsSuccess = false,
            MessageCodes = code,
            Message = message
        };
    }
}
