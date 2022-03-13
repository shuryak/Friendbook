using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IMessagesService
{
    Message? Send(Message message);
    Chat? CreateChat(string chatName, int creatorId);
    bool AddChatMember(int chatId, int memberId, int memberToAddId);
    bool IsChatMember(int chatId, int memberId);
    IEnumerable<Message>? GetList(int chatId, int memberId, int start, int offset);
}
