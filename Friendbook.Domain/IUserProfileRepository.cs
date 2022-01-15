using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IUserProfileRepository
{
    void Create(UserProfile userProfile);
    IEnumerable<UserProfile> GetList();
    UserProfile Get(int id);
    void Update(UserProfile entity);
    void Delete(int id);
}
