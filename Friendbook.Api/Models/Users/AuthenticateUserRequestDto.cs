using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models.Users;

public class AuthenticateUserRequestDto
{
    [Required]
    public string Nickname { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
