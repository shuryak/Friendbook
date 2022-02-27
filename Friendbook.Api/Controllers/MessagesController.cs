using Friendbook.Api.Helpers;
using Friendbook.Api.Hubs;
using Friendbook.Api.Models.Messages;
using Friendbook.Domain.Models;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("api/[controller].[action]")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly IUserProfileService _userProfileService;
    private readonly IHubContext<MessagesHub> _hubContext;
    
    public MessagesController(IMessagesService messagesService, IUserProfileService userProfileService, IHubContext<MessagesHub> hubContext)
    {
        _messagesService = messagesService;
        _userProfileService = userProfileService;
        _hubContext = hubContext;
    }

    [HttpPost]
    [Authorize]
    public ActionResult<Chat> CreateChat(CreateChatDto dto)
    {
        return _messagesService.CreateChat(new Chat(dto.ChatName));
    }
    
    [HttpPost]
    [Authorize]
    public ActionResult<bool> AddChatMember(CreateChatMemberDto dto)
    {
        UserProfile? httpContextUser = (UserProfile)HttpContext.Items["User"]!;

        return _messagesService.AddChatMember(dto.ChatId, httpContextUser.Id);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<bool>> Send(SendMessageDto dto)
    {
        UserProfile? httpContextUser = (UserProfile)HttpContext.Items["User"]!;

        Message? sentMessage = _messagesService.Send(new Message(dto.ChatId, httpContextUser.Id, dto.Text));
        
        await _hubContext.Clients.All.SendAsync("Send", sentMessage);

        return sentMessage != null;
    }
}
