using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IChatsRepository
{
    Chat Create(Chat chat);
    Chat? GetById(int id);
    bool IsJoined(int chatId, int userProfileId);
    void AddMember(int chatId, int userProfileId);
}
