using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Friendbook.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Friendbook.Api.Configuration;

public class JwtConfiguration
{
    public string Secret { get; set; } = string.Empty;
}
