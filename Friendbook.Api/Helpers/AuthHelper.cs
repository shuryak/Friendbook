using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Friendbook.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Friendbook.Api.Helpers;

public static class AuthHelper
{
    public static string GenerateJwtToken(this IConfiguration configuration, User user)
    {
        string issuer = configuration["JwtConfiguration:Issuer"];
        string audience = configuration["JwtConfiguration:Audience"];
        byte[] secret = Encoding.ASCII.GetBytes(configuration["JwtConfiguration:Secret"]);
        
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Nickname)
        };

        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(1)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256
                )
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
