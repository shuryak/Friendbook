using System.Security.Claims;
using Friendbook.Api.Hubs;
using Friendbook.Api.Models.Messages;
using Friendbook.Domain.Models;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("api/[controller].[action]")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly IUserService _userService;
    private readonly IHubContext<MessagesHub> _hubContext;
    
    public MessagesController(IMessagesService messagesService, IUserService userService, IHubContext<MessagesHub> hubContext)
    {
        _messagesService = messagesService;
        _userService = userService;
        _hubContext = hubContext;
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Chat>? CreateChat(CreateChatDto dto)
    {
        string? userNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string? userNickname = User.Identity?.Name;

        if (userNameIdentifier == null || userNickname == null)
        {
            return null;
        }

        int userId = Convert.ToInt32(userNameIdentifier);
        
        return _messagesService.CreateChat(dto.ChatName, userId);
    }
    
    [HttpPost]
    [Authorize]
    public ActionResult<bool>? AddChatMember(CreateChatMemberDto dto)
    {
        string? userNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string? userNickname = User.Identity?.Name;

        if (userNameIdentifier == null || userNickname == null)
        {
            return null;
        }

        int userId = Convert.ToInt32(userNameIdentifier);

        return _messagesService.AddChatMember(dto.ChatId, userId);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<bool>?> Send(SendMessageDto dto)
    {
        string? userNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string? userNickname = User.Identity?.Name;

        if (userNameIdentifier == null || userNickname == null)
        {
            return null;
        }

        int userId = Convert.ToInt32(userNameIdentifier);

        Message? sentMessage = _messagesService.Send(new Message(dto.ChatId, userId, dto.Text));
        
        await _hubContext.Clients.All.SendAsync("Send", sentMessage);

        return sentMessage != null;
    }
}
