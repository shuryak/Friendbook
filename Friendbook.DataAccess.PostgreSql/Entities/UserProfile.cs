using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities;

public class UserProfile
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Nickname { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public DateOnly DateOfBirth { get; set; }
}
