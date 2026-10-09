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
    public class ContentKeywordService : IContentKeywordService
    {
        private readonly IContentKeywordRepository _contentKeywordRepository;

        public ContentKeywordService(IContentKeywordRepository contentKeywordRepository)
        {
            _contentKeywordRepository = contentKeywordRepository;
        }

        public async Task<ServiceResponse<List<ContentKeywordDto>>> GetByContentAsync(int contentId)
        {
            var repoResponse = await _contentKeywordRepository.GetByContentAsync(contentId);

            if (repoResponse.OperationStatusCode == 50200)
                return Fail<List<ContentKeywordDto>>(MessageCodes.NotFound, "No se encontro el contenido indicado.");

            if (repoResponse.OperationStatusCode != 0)
                return Fail<List<ContentKeywordDto>>(MessageCodes.ErrorDataBase, "Ocurrio un error al consultar las palabras clave del contenido.");

            return new ServiceResponse<List<ContentKeywordDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Palabras clave obtenidas correctamente."
            };
        }

        public async Task<ServiceResponse<ContentKeywordDto>> AddAsync(int contentId, AddContentKeywordDto request)
        {
            var name = string.IsNullOrWhiteSpace(request.Name) ? null : request.Name.Trim();

            if (request.KeywordId is null && name is null)
                return Fail<ContentKeywordDto>(MessageCodes.ErrorValidation , "Debes enviar keywordId o name.");

            var repoResponse = await _contentKeywordRepository.AddAsync(contentId, request.KeywordId, name);

            return repoResponse.OperationStatusCode switch
            {
                0 => new ServiceResponse<ContentKeywordDto> { Data = MapDto(repoResponse.Data!), IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Palabra clave asociada correctamente." },
                50200 => Fail<ContentKeywordDto>(MessageCodes.NotFound, "No se encontro el contenido indicado."),
                50210 => Fail<ContentKeywordDto>(MessageCodes.NotFound, "No se encontro la palabra clave indicada."),
                50213 => Fail<ContentKeywordDto>(MessageCodes.ErrorValidation, "El nombre no puede estar vacio ni contener comas."),
                50221 => Fail<ContentKeywordDto>(MessageCodes.Conflict, "La palabra clave ya esta asociada a este contenido."),
                _ => Fail<ContentKeywordDto>(MessageCodes.ErrorDataBase, "Ocurrio un error inesperado al asociar la palabra clave.")
            };
        }

        public async Task<ServiceResponse<bool>> RemoveAsync(int contentId, int keywordId)
        {
            var repoResponse = await _contentKeywordRepository.RemoveAsync(contentId, keywordId);

            return repoResponse.OperationStatusCode switch
            {
                0 => new ServiceResponse<bool> { Data = true, IsSuccess = true, MessageCodes = MessageCodes.Success, Message = "Palabra clave desasociada correctamente." },
                50220 => Fail<bool>(MessageCodes.NotFound, "Ese contenido no tiene asociada esa palabra clave."),
                _ => Fail<bool>(MessageCodes.ErrorDataBase, "Ocurrio un error inesperado al desasociar la palabra clave.")
            };
        }

        private static ContentKeywordDto MapDto(ContentKeyWord ck) => new()
        {
            Id = ck.Id,
            ContentId = ck.ContentId,
            KeywordId = ck.KeywordId,
            KeywordName = ck.KeywordName
        };

        private static ServiceResponse<T> Fail<T>(MessageCodes code, string message) => new()
        {
            Data = default,
            IsSuccess = false,
            MessageCodes = code,
            Message = message
        };
    }
}
