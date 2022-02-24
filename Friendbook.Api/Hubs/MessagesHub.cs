using Friendbook.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace Friendbook.Api.Hubs;

public class MessagesHub : Hub
{
    public async Task Send(Message message)
    {
        await Clients.All.SendAsync("Send", message);
    }
}
