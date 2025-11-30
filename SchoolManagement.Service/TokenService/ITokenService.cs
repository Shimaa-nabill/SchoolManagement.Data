using SchoolManagement.Data.Entities;
using SchoolManagement.Service.AuthService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Service.TokenService
{
    public interface ITokenService
    {
        Task<AuthResponseDto> GenerateTokensAsync(ApplicationUser user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
