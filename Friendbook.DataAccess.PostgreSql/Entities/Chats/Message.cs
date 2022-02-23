using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities.Chats;

public class Message
{
    [Key]
    public int Id { get; set; }
    
    public int ChatId { get; set; }
    
    public Chat? Chat { get; set; }
    
    public string Text { get; set; } = string.Empty;
    
    public DateTime SentAt { get; set; }
}
