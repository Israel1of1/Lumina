using Business.DTOs;
using Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IAuthService
    {
     
            Task<ServiceResponse<LoginResponseDto>> LoginAsync(LoginRequestDto loginRequest);
            Task<ServiceResponse<LinkCodeInfoDto>> ValidateLinkCodeAsync(string code);
            Task<ServiceResponse<LoginResponseDto>> RegisterWithLinkCodeAsync(RegisterUserDto request);
        

    }
}
