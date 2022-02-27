using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;

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

    public Chat CreateChat(Chat chat)
    {
        return _chatsRepository.Create(chat);
    }

    public bool AddChatMember(int chatId, int memberId)
    {
        if (_chatsRepository.GetById(chatId) == null)
        {
            return false;
        };
        
        if (_chatsRepository.IsJoined(chatId, memberId))
        {
            return false;
        }
        
        _chatsRepository.AddMember(chatId, memberId);

        return true;
    }
    
    public Message? Send(Message message)
    {
        Chat? chat = _chatsRepository.GetById(message.ChatId);
        bool isJoined = _chatsRepository.IsJoined(message.ChatId, message.SenderId);
        
        if (chat == null || !isJoined)
        {
            return null;
        }

        return _messagesRepository.Create(message);
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
