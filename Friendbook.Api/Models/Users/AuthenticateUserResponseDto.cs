namespace Friendbook.Api.Models.Users;

public class AuthenticateUserResponseDto
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;
    
    public DateTime RefreshTokenExpiresAt { get; set; }
}
