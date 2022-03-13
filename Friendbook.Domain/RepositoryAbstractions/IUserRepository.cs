using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IUserRepository
{
    void Create(User user);
    IEnumerable<User> GetList(int offset, int limit);
    User? GetById(int id);
    User? GetByNickname(string nickname);
    IEnumerable<User> GetManyByIds(int[] ids);
    void Update(User entity);
    void Delete(int id);
}
