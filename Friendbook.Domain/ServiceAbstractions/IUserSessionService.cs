using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IUserSessionService
{
    public UserSession Create(User user, TimeSpan expiresIn);

    public UserSession? GetById(int sessionId);
    
    public UserSession? Renew(string refreshToken, TimeSpan expiresIn);
}
