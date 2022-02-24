using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Friendbook.Api.Configuration;
using Friendbook.Domain;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Friendbook.Api.Helpers;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IOptions<JwtConfiguration> _jwtConfiguration;

    public JwtMiddleware(RequestDelegate next, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _next = next;
        _jwtConfiguration = jwtConfiguration;
    }

    public async Task Invoke(HttpContext context, IUserProfileService userProfileService)
    {
        string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            AttachUserToContext(context, userProfileService, token);

        await _next(context);
    }

    private void AttachUserToContext(HttpContext context, IUserProfileService userProfileService, string token)
    {
        JwtSecurityTokenHandler tokenHandler = new();
        
        byte[] key = Encoding.ASCII.GetBytes(_jwtConfiguration.Value.Secret);
        
        tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            },
            out SecurityToken validatedToken
            );

        JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
        int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        context.Items["User"] = userProfileService.GetById(userId);
    }
}

public static class JwtMiddlewareExtensions
{
    public static void UseJwtMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<JwtMiddleware>();
    }
}
