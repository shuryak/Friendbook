using Friendbook.Domain;
using Friendbook.Domain.Models;

namespace Friendbook.BusinessLogic;

public class MessagesService : IMessagesService
{
    private readonly IChatsRepository _chatsRepository;
    private readonly IMessagesRepository _messagesRepository;

    public MessagesService(IChatsRepository chatsRepository, IMessagesRepository messagesRepository)
    {
        _chatsRepository = chatsRepository;
        _messagesRepository = messagesRepository;
    }

    public bool Send(Message message)
    {
        _chatsRepository.Create(new Chat
        {
            ChatName = "Chat " + message.ChatId
        });
        
        _messagesRepository.Create(message);

        return true;
    }

    public Message GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Message> GetList(int start, int offset)
    {
        throw new NotImplementedException();
    }
}
