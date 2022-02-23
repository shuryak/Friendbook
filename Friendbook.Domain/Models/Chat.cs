namespace Friendbook.Domain.Models;

public class Chat
{
    public int Id { get; set; }

    public string ChatName { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
}
