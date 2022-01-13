using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IUserProfileService
{
    bool Create(UserProfile userProfile);
    UserProfile Get(int id);
}
