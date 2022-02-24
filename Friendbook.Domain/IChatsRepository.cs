using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IChatsRepository
{
    int Create(Chat chat);
    Chat GetById(int id);
    void AddMember(int chatId, UserProfile userProfile);
}
