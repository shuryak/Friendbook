using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IMessagesService
{
    bool Send(Message message);
    Chat CreateChat(Chat chat);
    bool AddChatMember(int chatId, int memberId);
    Message GetById(int id);
    IEnumerable<Message> GetList(int start, int offset);
}
