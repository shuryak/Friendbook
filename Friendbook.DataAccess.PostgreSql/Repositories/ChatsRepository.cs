using AutoMapper;
using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain.RepositoryAbstractions;
using Chat = Friendbook.Domain.Models.Chat;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class ChatsRepository : IChatsRepository
{
    private readonly FriendbookDbContext _dbContext;
    private readonly IMapper _mapper;

    public ChatsRepository(FriendbookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Chat Create(string chatName, int creatorId)
    {
        DateTime dateTime = DateTime.UtcNow;

        Entities.Chats.Chat chatEntity = new Entities.Chats.Chat
        {
            ChatName = chatName,
            CreatedAt = dateTime
        };

        _dbContext.Chats.Add(chatEntity);
        _dbContext.ChatMembers.Add(new ChatMember
        {
            Chat = chatEntity,
            MemberId = creatorId,
            Role = "creator",
            InvitedAt = dateTime
        });
        _dbContext.SaveChanges();

        return _mapper.Map<Chat>(chatEntity);
    }

    public Chat? GetById(int id)
    {
        Entities.Chats.Chat? chatEntity = _dbContext.Chats.FirstOrDefault(x => x.Id == id);

        return chatEntity == null ? null : _mapper.Map<Chat>(chatEntity);
    }

    public bool IsJoined(int chatId, int userId)
    {
        return _dbContext.Chats
            .Where(x => x.Id == chatId)
            .Select(x => x.ChatMembers != null && x.ChatMembers.Any(chatMember => chatMember.MemberId == userId))
            .FirstOrDefault();
    }

    public void AddMember(int chatId, int userId)
    {
        _dbContext.ChatMembers.Add(new ChatMember
        {
            ChatId = chatId,
            MemberId = userId,
            Role = "member",
            InvitedAt = DateTime.UtcNow
        });

        _dbContext.SaveChanges();
    }
}
