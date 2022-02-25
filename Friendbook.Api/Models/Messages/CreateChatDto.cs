using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models.Messages;

public class CreateChatDto
{
    public CreateChatDto(string chatName)
    {
        ChatName = chatName;
    }
    
    [Required]
    public string ChatName { get; set; }
}
