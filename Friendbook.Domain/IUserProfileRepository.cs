using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IUserProfileRepository
{
    void Create(UserProfile userProfile);
    IEnumerable<UserProfile> GetList();
    UserProfile GetById(int id);
    UserProfile GetByNickname(string nickname);
    IEnumerable<UserProfile> GetManyByIds(int[] ids);
    void Update(UserProfile entity);
    void Delete(int id);
}
