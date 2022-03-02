using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public string Nickname { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public List<UserSession>? UserSessions { get; set; } = new();
}
