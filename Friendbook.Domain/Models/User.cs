namespace Friendbook.Domain.Models;

public class User
{
    public User(string nickname, string firstName, string lastName, int yearOfBirth, int monthOfBirth, int dayOfBirth)
    {
        Nickname = nickname;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = new DateOnly(yearOfBirth, monthOfBirth, dayOfBirth);
    }

    public User()
    {
    }
    
    public int Id { get; set; }
    
    public string Nickname { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateOnly DateOfBirth { get; set; }
    
    public string PasswordHash { get; set; }
}
