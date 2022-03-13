namespace Friendbook.Domain.Models;

public class UserSession
{
    public int SessionId { get; set; }
    
    public int UserId { get; set; }

    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTime ExpiresAt { get; set; }
}
