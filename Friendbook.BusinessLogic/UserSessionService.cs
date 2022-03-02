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

    public UserSession GetById(int sessionId)
    {
        return _userSessionRepository.GetById(sessionId);
    }

    public UserSession Renew(UserSession userSession, TimeSpan expiresIn)
    {
        return _userSessionRepository.Update(userSession, expiresIn);
    }
}
