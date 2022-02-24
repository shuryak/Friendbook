using AutoMapper;
using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain;
using Friendbook.Domain.Models;
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

    public int Create(Chat chat)
    {
        Entities.Chats.Chat chatEntity = _mapper.Map<Entities.Chats.Chat>(chat);

        _dbContext.Chats.Add(chatEntity);
        _dbContext.SaveChanges();

        return chatEntity.Id;
    }

    public Chat GetById(int id)
    {
        Entities.Chats.Chat? chatEntity = _dbContext.Chats.FirstOrDefault(x => x.Id == id);

        return _mapper.Map<Chat>(chatEntity);
    }

    public void AddMember(int chatId, UserProfile userProfile)
    {
        _dbContext.ChatMembers.Add(new ChatMember
        {
            ChatId = chatId,
            MemberId = userProfile.Id
        });

        _dbContext.SaveChanges();
    }
}
