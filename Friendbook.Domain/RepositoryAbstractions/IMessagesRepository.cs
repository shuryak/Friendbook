using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IMessagesRepository
{
    Message? Create(Message message);
    IEnumerable<Message> GetList(int offset, int limit);
    Message GetById(int id);
}
