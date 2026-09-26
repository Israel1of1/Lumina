using Business.DTOs;
using Core.Common;
using Core.Entities;
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
            Task<ServiceResponse<bool>> ChangePasswordAsync(int userId, ChangePasswordDto request);

    }
}
