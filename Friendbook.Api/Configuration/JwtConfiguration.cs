using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Friendbook.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Friendbook.Api.Configuration;

public class JwtConfiguration
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}
