using System.Security.Authentication;
using System.Security.Claims;
using Friendbook.Api.Models.RealTime;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Friendbook.Api.Hubs;

[SignalRHub]
[Authorize]
public class MessagesHub : Hub
{
    private readonly IMessagesService _messagesService;
    
    public MessagesHub(IMessagesService messagesService)
    {
        _messagesService = messagesService;
    }
    
    public override async Task OnConnectedAsync()
    {
        await Clients.Client(Context.ConnectionId).SendAsync("hello", new HelloMessage(Context.ConnectionId));
        await base.OnConnectedAsync();
    }

    public async Task SubscribeToChat(int chatId)
    {
        string? userNameIdentifier = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userNameIdentifier == null)
        {
            throw new AuthenticationException();
        }

        int userId = Convert.ToInt32(userNameIdentifier);

        if (_messagesService.IsChatMember(chatId, userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"chat{chatId}");
            await Clients.Client(Context.ConnectionId).SendAsync("error", new SuccessfulRequest("Ok!"));
        }
        else
        {
            await Clients.Client(Context.ConnectionId).SendAsync("error", new InvalidRequest("User is not in the chat"));   
        }
    }
}
