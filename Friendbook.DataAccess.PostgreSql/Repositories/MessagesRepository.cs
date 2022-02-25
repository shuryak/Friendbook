using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain;
using Friendbook.Domain.RepositoryAbstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Message = Friendbook.Domain.Models.Message;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class MessagesRepository : IMessagesRepository
{
    private readonly FriendbookDbContext _dbContext;

    public MessagesRepository(FriendbookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(Message message)
    {
        ChatMember? chatMember = _dbContext.ChatMembers.FirstOrDefault(x => x.ChatId == message.ChatId);

        if (chatMember == null) return;
        
        _dbContext.Messages.Add(new Entities.Chats.Message
        {
            ChatId = message.ChatId,
            ChatMember = chatMember,
            Text = message.Text,
            SentAt = DateTime.UtcNow
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
