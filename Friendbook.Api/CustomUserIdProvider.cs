using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;

namespace Friendbook.Api;

public class CustomUserIdProvider : IUserIdProvider
{
    public virtual string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
