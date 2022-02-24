using Friendbook.Api.Helpers;
using Friendbook.Api.Models;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.ServiceAbstractions;
using Microsoft.AspNetCore.Mvc;

namespace Friendbook.Api.Controllers;

[ApiController]
[Route("api/[controller].[action]")]
public class MessagesController : ControllerBase
{
    private readonly IMessagesService _messagesService;
    private readonly IUserProfileService _userProfileService;

    public MessagesController(IMessagesService messagesService, IUserProfileService userProfileService)
    {
        _messagesService = messagesService;
        _userProfileService = userProfileService;
    }
    
    [HttpPost]
    [Authorize]
    public ActionResult<bool> Send(SendMessageDto dto)
    {
        UserProfile? httpContextUser = (UserProfile)HttpContext.Items["User"]!;

        return _messagesService.Send(new Message
        {
            SenderId = httpContextUser.Id,
            Text = dto.Text,
            ChatId = dto.ChatId
        });
    }
}
