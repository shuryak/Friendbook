using Friendbook.Api.Helpers;
using Friendbook.Api.Models.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace Friendbook.Api.Hubs;

public class MessagesHub : Hub
{
    [Authorize]
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("Hello", new HelloMessage(Context.ConnectionId));
        await base.OnConnectedAsync();
    }
}
