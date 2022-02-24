using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IMessagesRepository
{
    void Create(Message message);
    IEnumerable<Message> GetList(int offset, int limit);
    Message GetById(int id);
}
