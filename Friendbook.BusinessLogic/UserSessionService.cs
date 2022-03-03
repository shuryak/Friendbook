using Friendbook.Domain;
using Friendbook.Domain.Models;

namespace Friendbook.BusinessLogic;

public class UserSessionService : IUserSessionService
{
    private readonly IUserSessionRepository _userSessionRepository;
    
    public UserSessionService(IUserSessionRepository userSessionRepository)
    {
        _userSessionRepository = userSessionRepository;
    }
    
    public UserSession Create(User user, TimeSpan expiresIn)
    {
        return _userSessionRepository.Create(user, expiresIn);
    }

    public UserSession? GetById(int sessionId)
    {
        UserSession? userSession = _userSessionRepository.GetById(sessionId);
        
        if (userSession == null || userSession.ExpiresAt < DateTime.UtcNow)
        {
            return null;
        }

        return userSession;
    }

    public UserSession? Renew(string refreshToken, TimeSpan expiresIn)
    {
        UserSession? userSession = _userSessionRepository.Update(refreshToken, expiresIn);

        if (userSession == null || userSession.ExpiresAt < DateTime.UtcNow)
        {
            return null;
        }

        return userSession;
    }
}
