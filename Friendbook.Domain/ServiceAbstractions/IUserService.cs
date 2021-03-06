using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IUserService
{
    bool Create(User user);
    User? GetById(int id);
    User? GetByNickname(string nickname);
    IEnumerable<User> GetList(int start, int offset);
}
