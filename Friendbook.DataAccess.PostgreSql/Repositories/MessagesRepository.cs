using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain;
using Message = Friendbook.Domain.Models.Message;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly FriendbookDbContext _dbContext;
    
    public void Create(Message message)
    {
        _dbContext.Messages.Add(new Entities.Chats.Message
        {
            ChatId = message.ChatId,
            Text = message.Text,
            SentAt = DateTime.Now
        });

        _dbContext.SaveChanges();
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
