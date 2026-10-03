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
    public class KeywordService : IKeywordService
    {
        private const int MaxPageSize = 100;
        private readonly IKeywordRepository _keywordRepository;

        public KeywordService(IKeywordRepository keywordRepository)
        {
            _keywordRepository = keywordRepository;
        }

        public async Task<ServiceResponse<PagedResultDto<KeywordDto>>> GetAllAsync(int pageNumber, int pageSize, string? search)
        {
            pageNumber = Math.Max(1, pageNumber);
            pageSize = Math.Clamp(pageSize, 1, MaxPageSize);
            search = string.IsNullOrWhiteSpace(search) ? null : search.Trim();

            var repoResponse = await _keywordRepository.GetAllAsync(pageNumber, pageSize, search);

            if (repoResponse.OperationStatusCode != 0)
                return Fail<PagedResultDto<KeywordDto>>(MessageCodes.ErrorDataBase, "Ocurrio un error al consultar las palabras clave.");

            var (items, totalRecords) = repoResponse.Data;

            return new ServiceResponse<PagedResultDto<KeywordDto>>
            {
                Data = new PagedResultDto<KeywordDto>
                {
                    Items = items.Select(MapDto).ToList(),
                    TotalRecords = totalRecords,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Palabras clave obtenidas correctamente."
            };
        }

        public async Task<ServiceResponse<KeywordDto>> GetByIdAsync(int id)
        {
            var repoResponse = await _keywordRepository.GetByIdAsync(id);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "consultada");
        }

        public async Task<ServiceResponse<KeywordDto>> CreateAsync(CreateKeywordDto request)
        {
            var repoResponse = await _keywordRepository.CreateAsync(request.Name.Trim());
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creada");
        }

        public async Task<ServiceResponse<KeywordDto>> UpdateAsync(int id, UpdateKeywordDto request)
        {
            var repoResponse = await _keywordRepository.UpdateAsync(id, request.Name.Trim());
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizada");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var repoResponse = await _keywordRepository.DeleteAsync(id);

            return repoResponse.OperationStatusCode switch
            {
                0 => new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Palabra clave eliminada correctamente." },
                50210 => Fail<bool>(MessageCodes.NotFound, "No se encontro la palabra clave indicada."),
                50212 => Fail<bool>(MessageCodes.Conflict, "No se puede eliminar: la palabra clave esta asociada a uno o mas contenidos."),
                _ => Fail<bool>(MessageCodes.ErrorDataBase, "Ocurrio un error inesperado al eliminar la palabra clave.")
            };
        }

        private static KeywordDto MapDto(Keyword k) => new()
        {
            Id = k.Id,
            Name = k.Name,
            UsageCount = k.UsageCount,
            CreatedAt = k.CreatedAt
        };

        private static ServiceResponse<KeywordDto> MapResponse(int statusCode, Keyword? data, string action)
        {
            return statusCode switch
            {
                0 => new ServiceResponse<KeywordDto> { Data = MapDto(data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = $"Palabra clave {action} correctamente." },
                50210 => Fail<KeywordDto>(MessageCodes.NotFound, "No se encontro la palabra clave indicada."),
                50211 => Fail<KeywordDto>(MessageCodes.Conflict, "Ya existe una palabra clave con ese nombre."),
                50213 => Fail<KeywordDto>(MessageCodes.ErrorValidation, "El nombre no puede estar vacio ni contener comas."),
                _ => Fail<KeywordDto>(MessageCodes.ErrorDataBase, "Ocurrio un error inesperado.")
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
