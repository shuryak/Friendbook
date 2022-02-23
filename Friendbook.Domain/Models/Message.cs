namespace Friendbook.Domain.Models;

public class Message
{
    public int Id { get; set; }
    
    public int ChatId { get; set; }
    
    public int SenderId { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime SentAt { get; set; }
}
