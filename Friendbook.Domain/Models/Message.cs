namespace Friendbook.Domain.Models;

public class Message
{
    public Message(int chatId, int senderId, string text)
    {
        ChatId = chatId;
        SenderId = senderId;
        Text = text;
    }
    
    public int Id { get; }
    
    public int ChatId { get; set; }
    
    public int SenderId { get; set; }

    public string Text { get; set; }

    public DateTime SentAt { get; } = DateTime.UtcNow;
}
