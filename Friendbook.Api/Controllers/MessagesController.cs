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
    public ActionResult<Chat> CreateChat(CreateChatDto dto)
    {
        User httpContextUser = (User)HttpContext.Items["User"]!;

        return _messagesService.CreateChat(new Chat(dto.ChatName, httpContextUser.Id));
    }
    
    [HttpPost]
    [Authorize]
    public ActionResult<bool> AddChatMember(CreateChatMemberDto dto)
    {
        User? httpContextUser = (User)HttpContext.Items["User"]!;

        return _messagesService.AddChatMember(dto.ChatId, httpContextUser.Id);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<bool>> Send(SendMessageDto dto)
    {
        User? httpContextUser = (User)HttpContext.Items["User"]!;

        Message? sentMessage = _messagesService.Send(new Message(dto.ChatId, httpContextUser.Id, dto.Text));
        
        await _hubContext.Clients.All.SendAsync("Send", sentMessage);

        return sentMessage != null;
    }
}
