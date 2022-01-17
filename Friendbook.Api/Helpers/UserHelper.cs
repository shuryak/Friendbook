using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Friendbook.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Friendbook.Api.Helpers;

public static class UserHelper
{
    public static string GenerateJwtToken(this IConfiguration configuration, UserProfile userProfile)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(configuration["JwtConfiguration:Secret"]);
        
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userProfile.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
