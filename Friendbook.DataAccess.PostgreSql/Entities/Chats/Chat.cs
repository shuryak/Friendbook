using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities.Chats;

public class Chat
{
    [Key]
    public int Id { get; set; }

    public string ChatName { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }

    public List<ChatMember>? ChatMembers { get; set; } = new();
}
