using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IMessagesService
{
    Message? Send(Message message);
    Chat CreateChat(string chatName, int creatorId);
    bool AddChatMember(int chatId, int memberId);
    bool IsChatMember(int chatId, int memberId);
    Message GetById(int id);
    IEnumerable<Message> GetList(int start, int offset);
}
