namespace Friendbook.Api.Models.Messages;

public class CreateChatDto
{
    public CreateChatDto(string chatName)
    {
        ChatName = chatName;
    }
    
    public string ChatName { get; set; }
}
