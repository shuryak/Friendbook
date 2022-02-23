using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities.Chats;

public class ChatMember
{
    [Key]
    public int Id { get; set; }
    
    public int ChatId { get; set; }
    
    public Chat? Chat { get; set; }
    
    public int MemberId { get; set; }
    
    public DateTime InvitedAt { get; set; }
}
