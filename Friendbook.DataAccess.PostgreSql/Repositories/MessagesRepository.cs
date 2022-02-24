using Friendbook.DataAccess.PostgreSql.Entities.Chats;
using Friendbook.Domain;
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
        EntityEntry<ChatMember> chatMember = _dbContext.ChatMembers.Add(new ChatMember
        {
            ChatId = message.ChatId,
            MemberId = message.SenderId,
            InvitedAt = DateTime.UtcNow
        });
        
        _dbContext.Messages.Add(new Entities.Chats.Message
        {
            ChatId = message.ChatId,
            SenderId = chatMember.Entity.Id,
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
