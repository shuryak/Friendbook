namespace Friendbook.Domain.Models;

public class UserProfile
{
    public UserProfile(string nickname, string firstName, string lastName, int yearOfBirth, int monthOfBirth, int dayOfBirth)
    {
        Nickname = nickname;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = new DateOnly(yearOfBirth, monthOfBirth, dayOfBirth);
    }
    
    public string Nickname { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateOnly DateOfBirth { get; set; }
}
