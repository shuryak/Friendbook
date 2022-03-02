using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models;

public class CreateUserDto
{
    [Required]
    public string Nickname { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public int DayOfBirth { get; set; }
    
    [Required]
    public int MonthOfBirth { get; set; }

    [Required]
    public int YearOfBirth { get; set; }

    [Required]
    public string Password { get; set; } = string.Empty;
}
