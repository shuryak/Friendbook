using System.Linq;
using AutoMapper;
using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain.RepositoryAbstractions;
using Message = Friendbook.Domain.Models.Message;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly FriendbookDbContext _dbContext;
    private readonly IMapper _mapper;

    public MessagesRepository(FriendbookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public Message? Create(Message message)
    {
        ChatMember? chatMember = _dbContext.ChatMembers.FirstOrDefault(x => x.ChatId == message.ChatId);

        if (chatMember == null) return null;

        Entities.Chats.Message messageEntity = new Entities.Chats.Message
        {
            ChatId = message.ChatId,
            ChatMember = chatMember,
            Text = message.Text,
            SentAt = DateTime.UtcNow
        };
        
        _dbContext.Messages.Add(messageEntity);

        _dbContext.SaveChanges();

        return new Message(messageEntity.Id, messageEntity.ChatId, messageEntity.ChatMember.MemberId, messageEntity.Text, messageEntity.SentAt);
    }

    public IEnumerable<Message> GetList(int chatId, int start, int offset)
    {
        List<Message> messages = _dbContext.Messages
            .Where(x => x.ChatId == chatId)
            .Skip(start)
            .Take(offset)
            .Select(message => _mapper.Map<Message>(message))
            .ToList();

        return messages;
    }
}
