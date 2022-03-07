using Friendbook.Domain.Models;

namespace Friendbook.Domain.RepositoryAbstractions;

public interface IChatsRepository
{
    Chat Create(string chatName, int creatorId);
    Chat? GetById(int id);
    bool IsJoined(int chatId, int userId);
    void AddMember(int chatId, int userId);
}
