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

    public class PecsCardService : IPecsCardService
    {
        private readonly IPecsCardRepository _cardRepository;
        private readonly IPecsBoardRepository _boardRepository;
        private readonly IAccessControlRepository _accessControlRepository;

        public PecsCardService(
            IPecsCardRepository cardRepository,
            IPecsBoardRepository boardRepository,
            IAccessControlRepository accessControlRepository)
        {
            _cardRepository = cardRepository;
            _boardRepository = boardRepository;
            _accessControlRepository = accessControlRepository;
        }

        public async Task<ServiceResponse<PecsCardDto>> CreateAsync(int requestingUserId, CreatePecsCardDto request)
        {
            var board = await _boardRepository.GetByIdAsync(request.BoardId);
            if (board.OperationStatusCode == 50210)
                return NotFound("No se encontro el tablero indicado.");

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, board.Data!.StudentId))
                return Denied<PecsCardDto>();

            var card = new PecsCard
            {
                BoardId = request.BoardId,
                Title = request.Title,
                ImageUrl = request.ImageUrl,
                AudioUrl = request.AudioUrl,
                Category = request.Category,
                OrderNumber = request.OrderNumber
            };

            var repoResponse = await _cardRepository.CreateAsync(card);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creada");
        }

        public async Task<ServiceResponse<List<PecsCardDto>>> GetByBoardAsync(int requestingUserId, int boardId)
        {
            var board = await _boardRepository.GetByIdAsync(boardId);
            if (board.OperationStatusCode == 50210)
                return new ServiceResponse<List<PecsCardDto>> {
                    Data = null, 
                    IsSuccess = false,
                    MessageCodes = MessageCodes.NotFound,
                    Message = "No se encontro el tablero indicado." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, board.Data!.StudentId))
                return Denied<List<PecsCardDto>>();

            var repoResponse = await _cardRepository.GetByBoardAsync(boardId);

            return new ServiceResponse<List<PecsCardDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Tarjetas obtenidas correctamente."
            };
        }

        public async Task<ServiceResponse<PecsCardDto>> UpdateAsync(int requestingUserId, int id, UpdatePecsCardDto request)
        {
            var (problem, _) = await CheckAccessByCardId(requestingUserId, id);
            if (problem == "notfound")
                return NotFound("No se encontro la tarjeta indicada.");
            if (problem == "denied")
                return Denied<PecsCardDto>();

            var card = new PecsCard { Title = request.Title, ImageUrl = request.ImageUrl, AudioUrl = request.AudioUrl, Category = request.Category, OrderNumber = request.OrderNumber };
            var repoResponse = await _cardRepository.UpdateAsync(id, card);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizada");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int requestingUserId, int id)
        {
            var (problem, _) = await CheckAccessByCardId(requestingUserId, id);
            if (problem == "notfound")
                return new ServiceResponse<bool> { 
                    Data = false, 
                    IsSuccess = false, 
                    MessageCodes = MessageCodes.NotFound,
                    Message = "No se encontro la tarjeta indicada." };
            if (problem == "denied")
                return new ServiceResponse<bool> { 
                    Data = false, 
                    IsSuccess = false,
                    MessageCodes = MessageCodes.Unauthorized, 
                    Message = "No tienes permiso para modificar esta tarjeta." };

            var repoResponse = await _cardRepository.DeleteAsync(id);

            return repoResponse.OperationStatusCode == 0
                ? new ServiceResponse<bool> {
                    Data = true, 
                    IsSuccess = true, 
                    MessageCodes = MessageCodes.Success, 
                    Message = "Tarjeta eliminada correctamente." }
                : new ServiceResponse<bool> {
                    Data = false, 
                    IsSuccess = false,
                    MessageCodes = MessageCodes.ErrorDataBase, 
                    Message = "Ocurrio un error inesperado." };
        }

        private async Task<(string? Problem, int StudentId)> CheckAccessByCardId(int requestingUserId, int cardId)
        {
            var cardResponse = await _cardRepository.GetByIdWithStudentAsync(cardId);
            if (cardResponse.OperationStatusCode == 50211)
                return ("notfound", 0);

            var hasAccess = await _accessControlRepository.HasStudentAccessAsync(requestingUserId, cardResponse.Data!.StudentId);
            return hasAccess ? (null, cardResponse.Data.StudentId) : ("denied", 0);
        }

        private static PecsCardDto MapDto(PecsCard c) => new()
        {
            Id = c.Id,
            BoardId = c.BoardId,
            Title = c.Title,
            ImageUrl = c.ImageUrl,
            AudioUrl = c.AudioUrl,
            Category = c.Category,
            OrderNumber = c.OrderNumber
        };

        private static ServiceResponse<PecsCardDto> MapResponse(int statusCode, PecsCard? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<PecsCardDto> {
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success, 
                        Message = $"Tarjeta {action} correctamente." };
                case 50210:
                    return NotFound("No se encontro el tablero indicado.");
                case 50211:
                    return NotFound("No se encontro la tarjeta indicada.");
                default:
                    return new ServiceResponse<PecsCardDto> { 
                        Data = null, 
                        IsSuccess = false, 
                        MessageCodes = MessageCodes.ErrorDataBase, 
                        Message = "Ocurrio un error inesperado." };
            }
        }

        private static ServiceResponse<PecsCardDto> NotFound(string message) => new() {
            Data = null, 
            IsSuccess = false,
            MessageCodes = MessageCodes.NotFound, 
            Message = message };

        private static ServiceResponse<T> Denied<T>() => new()
        {
            Data = default,
            IsSuccess = false,
            MessageCodes = MessageCodes.Unauthorized,
            Message = "No tienes permiso para acceder a la informacion de este estudiante."
        };
    }
}
