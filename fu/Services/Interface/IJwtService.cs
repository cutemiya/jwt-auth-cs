using System.Security.Claims;
using fu.Models.Dto;

namespace fu.Services.Interface;

public interface IJwtService
{
    string CreateToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0);
    Task<bool> CheckAccount(string email);
    Task<AuthDtoResponse> ExpireToken(string token);
}