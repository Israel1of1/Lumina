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

    public class PecsBoardService : IPecsBoardService
    {
        private readonly IPecsBoardRepository _boardRepository;
        private readonly IAccessControlRepository _accessControlRepository;

        public PecsBoardService(IPecsBoardRepository boardRepository, IAccessControlRepository accessControlRepository)
        {
            _boardRepository = boardRepository;
            _accessControlRepository = accessControlRepository;
        }

        public async Task<ServiceResponse<PecsBoardDto>> CreateAsync(int requestingUserId, CreatePecsBoardDto request)
        {
            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, request.StudentId))
                return Denied<PecsBoardDto>();

            var board = new PecsBoard { StudentId = request.StudentId, Name = request.Name, Description = request.Description };
            var repoResponse = await _boardRepository.CreateAsync(board);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "creado");
        }

        public async Task<ServiceResponse<List<PecsBoardDto>>> GetByStudentAsync(int requestingUserId, int studentId)
        {
            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, studentId))
                return Denied<List<PecsBoardDto>>();

            var repoResponse = await _boardRepository.GetByStudentAsync(studentId);

            if (repoResponse.OperationStatusCode == 50170)
                return new ServiceResponse<List<PecsBoardDto>> { 
                    Data = null,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.NotFound,
                    Message = "No se encontro el estudiante indicado." };

            return new ServiceResponse<List<PecsBoardDto>>
            {
                Data = repoResponse.Data.Select(MapDto).ToList(),
                IsSuccess = true,
                MessageCodes = MessageCodes.Success,
                Message = "Tableros obtenidos correctamente."
            };
        }

        public async Task<ServiceResponse<PecsBoardDto>> UpdateAsync(int requestingUserId, int id, UpdatePecsBoardDto request)
        {
            var existing = await _boardRepository.GetByIdAsync(id);
            if (existing.OperationStatusCode == 50210)
                return NotFound("No se encontro el tablero indicado.");

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, existing.Data!.StudentId))
                return Denied<PecsBoardDto>();

            var board = new PecsBoard { Name = request.Name, Description = request.Description };
            var repoResponse = await _boardRepository.UpdateAsync(id, board);
            return MapResponse(repoResponse.OperationStatusCode, repoResponse.Data, "actualizado");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int requestingUserId, int id)
        {
            var existing = await _boardRepository.GetByIdAsync(id);
            if (existing.OperationStatusCode == 50210)
                return new ServiceResponse<bool> { Data = false, 
                    IsSuccess = false, 
                    MessageCodes = MessageCodes.NotFound, 
                    Message = "No se encontro el tablero indicado." };

            if (!await _accessControlRepository.HasStudentAccessAsync(requestingUserId, existing.Data!.StudentId))
                return new ServiceResponse<bool> {
                    Data = false,
                    IsSuccess = false,
                    MessageCodes = MessageCodes.Unauthorized,
                    Message = "No tienes permiso para modificar este tablero." };

            var repoResponse = await _boardRepository.DeleteAsync(id);

            switch (repoResponse.OperationStatusCode)
            {
                case 0:
                    return new ServiceResponse<bool> {
                        Data = true, 
                        IsSuccess = true, 
                        MessageCodes = MessageCodes.Success, 
                        Message = "Tablero eliminado correctamente." };
                case 50210:
                    return new ServiceResponse<bool> { 
                        Data = false,
                        IsSuccess = false, 
                        MessageCodes = MessageCodes.NotFound,
                        Message = "No se encontro el tablero indicado." };
                case 50212:
                    return new ServiceResponse<bool> { 
                        Data = false, IsSuccess = false,
                        MessageCodes = MessageCodes.Conflict, 
                        Message = "No se puede eliminar: el tablero todavia tiene tarjetas." };
                default:
                    return new ServiceResponse<bool> {
                        Data = false, 
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase, 
                        Message = "Ocurrio un error inesperado." };
            }
        }

        private static PecsBoardDto MapDto(PecsBoard b) => new() { Id = b.Id, StudentId = b.StudentId, Name = b.Name, Description = b.Description };

        private static ServiceResponse<PecsBoardDto> MapResponse(int statusCode, PecsBoard? data, string action)
        {
            switch (statusCode)
            {
                case 0:
                    return new ServiceResponse<PecsBoardDto> { 
                        Data = MapDto(data!),
                        IsSuccess = true,
                        MessageCodes = MessageCodes.Success, 
                        Message = $"Tablero {action} correctamente." };
                case 50170:
                    return NotFound("No se encontro el estudiante indicado.");
                case 50210:
                    return NotFound("No se encontro el tablero indicado.");
                default:
                    return new ServiceResponse<PecsBoardDto> {
                        Data = null, 
                        IsSuccess = false,
                        MessageCodes = MessageCodes.ErrorDataBase, Message = "Ocurrio un error inesperado." };
            }
        }

        private static ServiceResponse<PecsBoardDto> NotFound(string message) => new() {
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
