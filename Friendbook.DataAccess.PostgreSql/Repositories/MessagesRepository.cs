using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain.RepositoryAbstractions;
using Message = Friendbook.Domain.Models.Message;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly FriendbookDbContext _dbContext;

    public MessagesRepository(FriendbookDbContext dbContext)
    {
        _dbContext = dbContext;
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

    public IEnumerable<Message> GetList(int offset, int limit)
    {
        throw new NotImplementedException();
    }

    public Message GetById(int id)
    {
        throw new NotImplementedException();
    }
}
