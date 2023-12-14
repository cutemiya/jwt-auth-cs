using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common;
using fu.Models.Base;
using fu.Models.Dto;
using fu.Repository.Interface;
using fu.Services.Interface;
using Microsoft.IdentityModel.Tokens;

namespace fu.Services;

public class JwtService: IJwtService
{
    private readonly IAccountRepository _accountRepository;
    
    public JwtService(IAccountRepository accountRepository)
    {
        this._accountRepository = accountRepository;
    }
    
    public string CreateToken(ICollection<Claim> claims, int tokenExpiresAfterHours = 0) 
    {
        var authSigningKey = AuthOptions.GetSymmetricSecurityKey();
        if (tokenExpiresAfterHours == 0)
        {
            tokenExpiresAfterHours = AuthOptions.TokenExpiresAfterHours;
        }

        var token = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            expires: DateTime.UtcNow.AddHours(tokenExpiresAfterHours),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
 
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> CheckAccount(string email)
    {
        return await _accountRepository.CheckAccount(email);
    }

    public async Task<AuthDtoResponse> ExpireToken(string token)
    {
        if (await _accountRepository.CheckRefreshToken(token))
            throw new Exception("token is not exist");

        var account = await _accountRepository.GetAccountDataByToken(token);
        if (account == null)
            throw new Exception("error on get account");
        
        var claims = Jwt.GetClaims(account.Id, account.Email, account.Username, account.Role);
        var refreshToken = CreateToken(new List<Claim>(), 72);
        var accessToken = CreateToken(claims, 72);

        await _accountRepository.UpdateRefresh(refreshToken, DateTime.Now.AddHours(72), account.Id);

        return new AuthDtoResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}