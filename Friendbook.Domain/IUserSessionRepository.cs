using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IUserSessionRepository
{
    public UserSession Create(User user, TimeSpan expiresIn);

    public UserSession? GetById(int sessionId);

    public UserSession? Update(string refreshToken, TimeSpan expiresIn);
}
