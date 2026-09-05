using Business.DTOs;
using Business.Interfaces;
using Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkCodeController : ControllerBase
    {

            private readonly ILinkCodeService _linkCodeService;

            public LinkCodeController(ILinkCodeService linkCodeService)
            {
                _linkCodeService = linkCodeService;
            }

            /// <summary>Genera el codigo de vinculacion para que un Docente cree su cuenta.</summary>
            [HttpPost("teacher")]
            public async Task<IActionResult> CreateForTeacher([FromBody] CreateTeacherLinkCodeDto request)
            {
                // issuedById quedaria en null mientras no exista el login de Institucion.
                var result = await _linkCodeService.CreateForTeacherAsync(request, issuedById: null);
                return MapResponse(result);
            }

            /// <summary>Genera el codigo de vinculacion para que un Tutor cree su cuenta.</summary>
            [HttpPost("guardian")]
            public async Task<IActionResult> CreateForGuardian([FromBody] CreateGuardianLinkCodeDto request)
            {
                var result = await _linkCodeService.CreateForGuardianAsync(request, issuedById: null);
                return MapResponse(result);
            }

            /// <summary>Revoca un codigo que aun no ha sido usado (por ejemplo, si se filtro).</summary>
            [HttpPost("{code}/revoke")]
            public async Task<IActionResult> Revoke(string code)
            {
                var result = await _linkCodeService.RevokeAsync(code);
                return MapResponse(result);
            }

            private IActionResult MapResponse<T>(ServiceResponse<T> result)
            {
                if (result.IsSuccess)
                    return Ok(result);

                return result.MessageCodes switch
                {
                    MessageCodes.ErrorValidation => BadRequest(result),
                    MessageCodes.Unauthorized => Unauthorized(result),
                    MessageCodes.NotFound => NotFound(result),
                    MessageCodes.Conflict => Conflict(result),
                    _ => StatusCode(500, result)
                };
            }
    }
}
