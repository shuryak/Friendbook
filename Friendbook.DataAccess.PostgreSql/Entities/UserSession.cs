using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities;

public class UserSession
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public User? User { get; set; }

    [Required]
    public string RefreshToken { get; set; } = string.Empty;

    [Required]
    public DateTime ExpiresAt { get; set; }
}
