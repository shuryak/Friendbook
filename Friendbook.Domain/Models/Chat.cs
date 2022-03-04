namespace Friendbook.Domain.Models;

public class Chat
{
    public Chat(string chatName, int creatorId)
    {
        ChatName = chatName;
        CreatorId = creatorId;
    }
    
    public int Id { get; set; }

    public int CreatorId { get; set; }
    
    public string ChatName { get; set; }
    
    public DateTime CreatedAt { get; }
}
