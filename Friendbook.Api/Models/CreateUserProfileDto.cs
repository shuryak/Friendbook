using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models;

public class CreateUserProfileDto
{
    [Required]
    public string Nickname { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }

    [Required]
    public int DayOfBirth { get; set; }
    
    [Required]
    public int MonthOfBirth { get; set; }

    [Required]
    public int YearOfBirth { get; set; }
}
