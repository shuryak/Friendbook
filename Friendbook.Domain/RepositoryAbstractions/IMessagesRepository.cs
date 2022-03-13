using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IMessagesRepository
{
    Message? Create(Message message);
    IEnumerable<Message> GetList(int chatId, int start, int offset);
}
