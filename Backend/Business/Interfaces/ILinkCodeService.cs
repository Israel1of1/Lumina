using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public  interface ILinkCodeService
    {
        Task<ServiceResponse<LinkCodeInfoDto>> CreateForTeacherAsync(CreateTeacherLinkCodeDto request, int? issuedById);
        Task<ServiceResponse<LinkCodeInfoDto>> CreateForGuardianAsync(CreateGuardianLinkCodeDto request, int? issuedById);
        Task<ServiceResponse<bool>> RevokeAsync(string code);
    }
}
