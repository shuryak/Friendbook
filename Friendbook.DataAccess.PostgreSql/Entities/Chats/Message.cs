using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Friendbook.DataAccess.PostgreSql.Entities.Chats;

public class Message
{
    [Key]
    public int Id { get; set; }
    
    public int ChatId { get; set; }
    
    public Chat? Chat { get; set; }
    
    public int? SenderId { get; set; }
    
    [ForeignKey("SenderId")]
    public ChatMember? ChatMember { get; set; }

    public string Text { get; set; } = string.Empty;
    
    public DateTime SentAt { get; set; }
}
