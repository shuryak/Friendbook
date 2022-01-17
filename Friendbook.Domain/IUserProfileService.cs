using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IUserProfileService
{
    bool Create(UserProfile userProfile);
    UserProfile GetById(int id);
    UserProfile GetByNickname(string nickname);
    IEnumerable<UserProfile> GetList(int start, int offset);
}
