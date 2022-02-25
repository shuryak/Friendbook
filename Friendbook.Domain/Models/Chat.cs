namespace Friendbook.Domain.Models;

public class Chat
{
    public Chat(string chatName)
    {
        ChatName = chatName;
    }
    
    public int Id { get; set; }

    public string ChatName { get; set; }
    
    public DateTime CreatedAt { get; }
}
