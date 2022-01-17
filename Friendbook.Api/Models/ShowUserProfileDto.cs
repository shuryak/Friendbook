namespace Friendbook.Api.Models;

public class ShowUserProfileDto
{
    public int Id { get; set; }

    public string Nickname { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public int DayOfBirth { get; set; }
    
    public int MonthOfBirth { get; set; }
    
    public int YearOfBirth { get; set; }
}
