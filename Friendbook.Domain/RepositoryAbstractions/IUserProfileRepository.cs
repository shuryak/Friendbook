using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IUserProfileRepository
{
    void Create(UserProfile userProfile);
    IEnumerable<UserProfile> GetList(int offset, int limit);
    UserProfile GetById(int id);
    UserProfile GetByNickname(string nickname);
    IEnumerable<UserProfile> GetManyByIds(int[] ids);
    void Update(UserProfile entity);
    void Delete(int id);
}