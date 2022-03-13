namespace Friendbook.Domain.Models;

public class Message
{
    public Message()
    {
    }
    
    public Message(int chatId, int senderId, string text)
    {
        ChatId = chatId;
        SenderId = senderId;
        Text = text;
    }

    public Message(int id, int chatId, int senderId, string text, DateTime sentAt)
    {
        Id = id;
        ChatId = chatId;
        SenderId = senderId;
        Text = text;
        SentAt = sentAt;
    }
    
    public int Id { get; set; }
    
    public int ChatId { get; set; }
    
    public int SenderId { get; set; }

    public string Text { get; set; }

    public DateTime SentAt { get; set;  }
}
