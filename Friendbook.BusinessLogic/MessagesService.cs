using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;

namespace Friendbook.BusinessLogic;

public class MessagesService : IMessagesService
{
    private readonly IChatsRepository _chatsRepository;
    private readonly IMessagesRepository _messagesRepository;
    private readonly IUserRepository _userRepository;

    public MessagesService(IChatsRepository chatsRepository, IMessagesRepository messagesRepository, IUserRepository userRepository)
    {
        _chatsRepository = chatsRepository;
        _messagesRepository = messagesRepository;
        _userRepository = userRepository;
    }
    
    public Message? Send(Message message)
    {
        bool isJoined = _chatsRepository.IsJoined(message.ChatId, message.SenderId);
        Chat? chat = _chatsRepository.GetById(message.ChatId);

        if (!isJoined || chat == null)
        {
            return null;
        }

        return _messagesRepository.Create(message);
    }

    public Chat? CreateChat(string chatName, int creatorId)
    {
        if (_userRepository.GetById(creatorId) == null)
        {
            return null;
        }
        
        return _chatsRepository.Create(chatName, creatorId);
    }

    public bool AddChatMember(int chatId, int memberId, int memberToAddId)
    {
        if (memberId == memberToAddId)
        {
            return false;
        }
        
        if (_chatsRepository.GetById(chatId) == null)
        {
            return false;
        }
        
        if (_userRepository.GetById(memberId) == null || _userRepository.GetById(memberToAddId) == null)
        {
            return false;
        }

        if (!_chatsRepository.IsJoined(chatId, memberId))
        {
            return false;
        }
        
        if (_chatsRepository.IsJoined(chatId, memberToAddId))
        {
            return false;
        }

        _chatsRepository.AddMember(chatId, memberToAddId);

        return true;
    }

    public bool IsChatMember(int chatId, int memberId)
    {
        return _chatsRepository.IsJoined(chatId, memberId);
    }

    public IEnumerable<Message>? GetList(int chatId, int memberId, int start, int offset)
    {
        if (!_chatsRepository.IsJoined(chatId, memberId))
        {
            return null;
        }
        
        return _messagesRepository.GetList(chatId, start, offset);
    }
}
