using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace fu.Models.Base;

public abstract class AuthOptions
{
    public const string Issuer = "MyAuthServer"; // издатель токена
    public const string Audience = "MyAuthClient"; // потребитель токена
    const string Key = "mysupersecret_secretkey!123"; // ключ для шифрации
    public const int TokenExpiresAfterHours = 72; // время жизни лол

    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}