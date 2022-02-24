using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IMessagesService
{
    bool Send(Message message);
    Message GetById(int id);
    IEnumerable<Message> GetList(int start, int offset);
}
