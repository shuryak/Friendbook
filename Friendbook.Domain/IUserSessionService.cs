using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IUserSessionService
{
    public UserSession Create(User user, TimeSpan expiresIn);

    public UserSession GetById(int sessionId);
    
    public UserSession Renew(UserSession userSession, TimeSpan expiresIn);
}
