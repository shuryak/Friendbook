using Friendbook.BusinessLogic;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class MessagesServiceTests
{
    private Mock<IChatsRepository> _chatsRepositoryMock = null!;
    private Mock<IUserRepository> _userRepositoryMock = null!;
    private Mock<IMessagesRepository> _messagesRepositoryMock = null!;
    private IMessagesService _messagesService = null!;

    [SetUp]
    public void SetUp()
    {
        _chatsRepositoryMock = new Mock<IChatsRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _messagesRepositoryMock = new Mock<IMessagesRepository>();
        _messagesService = new MessagesService(_chatsRepositoryMock.Object, _messagesRepositoryMock.Object,
            _userRepositoryMock.Object);
    }

    [Test]
    public void Send_ShouldReturnMessage()
    {
        // Arrange
        const int chatId = 1;
        const int senderId = 1;

        Message message = new Message(chatId, senderId, "something");

        _chatsRepositoryMock.Setup(x => x.GetById(chatId))
            .Returns(new Chat
            {
                Id = chatId,
                ChatName = "something",
                CreatedAt = DateTime.UtcNow - TimeSpan.FromHours(1)
            })
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, senderId))
            .Returns(true)
            .Verifiable();

        _messagesRepositoryMock.Setup(x => x.Create(message))
            .Returns(new Message
            {
                Id = 5,
                ChatId = chatId,
                SenderId = senderId,
                Text = "something",
                SentAt = DateTime.UtcNow - TimeSpan.FromMinutes(5)
            })
            .Verifiable();

        // Act
        Message? result = _messagesService.Send(message);

        // Assert
        _chatsRepositoryMock.Verify(x => x.GetById(chatId), Times.Once);
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, senderId), Times.Once);
        _messagesRepositoryMock.Verify(x => x.Create(message), Times.Once);
        Assert.IsNotNull(result);
    }

    [Test]
    public void Send_IsNotJoined_ShouldReturnNull()
    {
        // Arrange
        const int chatId = 1;
        const int senderId = 1;

        Message message = new Message(chatId, senderId, "something");

        _chatsRepositoryMock.Setup(x => x.GetById(chatId))
            .Returns((Chat?) null)
            .Verifiable();

        _messagesRepositoryMock.Setup(x => x.Create(message)).Verifiable();

        // Act
        Message? result = _messagesService.Send(message);

        // Assert
        _chatsRepositoryMock.Verify(x => x.GetById(chatId), Times.Once);
        _messagesRepositoryMock.Verify(x => x.Create(message), Times.Never);
        Assert.IsNull(result);
    }

    [Test]
    public void Send_ChatIsNotExists_ShouldReturnNull()
    {
        // Arrange
        const int chatId = 1;
        const int senderId = 1;

        Message message = new Message(chatId, senderId, "something");

        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, senderId))
            .Returns(false)
            .Verifiable();

        _messagesRepositoryMock.Setup(x => x.Create(message)).Verifiable();

        // Act
        Message? result = _messagesService.Send(message);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, senderId), Times.Once);
        _messagesRepositoryMock.Verify(x => x.Create(message), Times.Never);
        Assert.IsNull(result);
    }

    [Test]
    public void CreateChat_ShouldReturnChat()
    {
        // Arrange
        const string chatName = "something";
        const int creatorId = 1;

        _userRepositoryMock.Setup(x => x.GetById(creatorId))
            .Returns(new User())
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.Create(chatName, creatorId))
            .Returns(new Chat
            {
                Id = 1,
                ChatName = chatName,
                CreatedAt = DateTime.UtcNow
            })
            .Verifiable();

        // Act
        Chat? chat = _messagesService.CreateChat(chatName, creatorId);

        // Assert
        _userRepositoryMock.Verify(x => x.GetById(creatorId), Times.Once);
        _chatsRepositoryMock.Verify(x => x.Create(chatName, creatorId), Times.Once);
        Assert.IsNotNull(chat);
    }

    [Test]
    public void CreateChat_CreatorIsNotExists_ShouldReturnNull()
    {
        // Arrange
        const string chatName = "something";
        const int creatorId = 1;

        _userRepositoryMock.Setup(x => x.GetById(creatorId))
            .Returns((User?) null)
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.Create(chatName, creatorId)).Verifiable();

        // Act
        Chat? chat = _messagesService.CreateChat(chatName, creatorId);

        // Assert
        _userRepositoryMock.Verify(x => x.GetById(creatorId), Times.Once);
        _chatsRepositoryMock.Verify(x => x.Create(chatName, creatorId), Times.Never);
        Assert.IsNull(chat);
    }

    [Test]
    public void AddChatMember_ShouldReturnChatMember()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int memberToAddId = 2;

        _userRepositoryMock.Setup(x => x.GetById(memberId))
            .Returns(new User())
            .Verifiable();
        _userRepositoryMock.Setup(x => x.GetById(memberToAddId))
            .Returns(new User())
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.GetById(chatId))
            .Returns(new Chat())
            .Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(true)
            .Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberToAddId))
            .Returns(false)
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.AddMember(chatId, memberToAddId)).Verifiable();

        // Act
        bool result = _messagesService.AddChatMember(chatId, memberId, memberToAddId);

        // Assert
        _userRepositoryMock.Verify(x => x.GetById(memberId), Times.Once);
        _userRepositoryMock.Verify(x => x.GetById(memberToAddId), Times.Once);
        _chatsRepositoryMock.Verify(x => x.GetById(chatId), Times.Once);
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId), Times.Once);
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberToAddId), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    public void AddChatMember_MemberIdIsEqualToMemberToAddId_ShouldReturnFalse()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int memberToAddId = 1;

        _userRepositoryMock.Setup(x => x.GetById(memberId)).Verifiable();
        _userRepositoryMock.Setup(x => x.GetById(memberToAddId)).Verifiable();

        _chatsRepositoryMock.Setup(x => x.GetById(chatId)).Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId)).Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberToAddId)).Verifiable();

        _chatsRepositoryMock.Setup(x => x.AddMember(chatId, memberToAddId)).Verifiable();

        // Act
        bool result = _messagesService.AddChatMember(chatId, memberId, memberToAddId);

        // Assert
        _userRepositoryMock.Verify(x => x.GetById(memberId), Times.Never);
        _userRepositoryMock.Verify(x => x.GetById(memberToAddId), Times.Never);
        _chatsRepositoryMock.Verify(x => x.GetById(chatId), Times.Never);
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId), Times.Never);
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberToAddId), Times.Never);
        Assert.IsFalse(result);
    }

    [Test]
    public void AddChatMember_ChatIsNotExists_ShouldReturnFalse()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int memberToAddId = 2;

        _userRepositoryMock.Setup(x => x.GetById(memberId))
            .Returns(new User())
            .Verifiable();
        _userRepositoryMock.Setup(x => x.GetById(memberToAddId))
            .Returns(new User())
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.GetById(chatId))
            .Returns((Chat?) null)
            .Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId)).Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberToAddId)).Verifiable();

        _chatsRepositoryMock.Setup(x => x.AddMember(chatId, memberToAddId)).Verifiable();

        // Act
        bool result = _messagesService.AddChatMember(chatId, memberId, memberToAddId);

        // Assert
        _chatsRepositoryMock.Verify(x => x.GetById(chatId), Times.Once);
        Assert.IsFalse(result);
    }

    [TestCase(1, 2)]
    [TestCase(2, 1)]
    public void AddChatMember_SomeMemberIsNotExists_ShouldReturnFalse(int memberId, int memberToAddId)
    {
        // Arrange
        const int chatId = 1;

        _userRepositoryMock.Setup(x => x.GetById(memberId))
            .Returns((User?) null)
            .Verifiable();
        _userRepositoryMock.Setup(x => x.GetById(memberToAddId))
            .Returns(new User())
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.GetById(chatId)).Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId)).Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberToAddId)).Verifiable();

        _chatsRepositoryMock.Setup(x => x.AddMember(chatId, memberToAddId)).Verifiable();

        // Act
        bool result = _messagesService.AddChatMember(chatId, memberId, memberToAddId);

        // Assert
        _userRepositoryMock.Verify(x => x.GetById(memberId), Times.AtMostOnce);
        _userRepositoryMock.Verify(x => x.GetById(memberToAddId), Times.AtMostOnce);
        Assert.IsFalse(result);
    }

    [Test]
    public void AddChatMember_MemberIsNotJoined_ShouldReturnFalse()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int memberToAddId = 2;

        _userRepositoryMock.Setup(x => x.GetById(memberId))
            .Returns(new User())
            .Verifiable();
        _userRepositoryMock.Setup(x => x.GetById(memberToAddId))
            .Returns(new User())
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.GetById(chatId))
            .Returns(new Chat())
            .Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(false)
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.AddMember(chatId, memberToAddId)).Verifiable();

        // Act
        bool result = _messagesService.AddChatMember(chatId, memberId, memberToAddId);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId), Times.Once);
        Assert.IsFalse(result);
    }

    [Test]
    public void AddChatMember_MemberToAddAlreadyJoined_ShouldReturnFalse()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int memberToAddId = 2;

        _userRepositoryMock.Setup(x => x.GetById(memberId))
            .Returns(new User())
            .Verifiable();
        _userRepositoryMock.Setup(x => x.GetById(memberToAddId))
            .Returns(new User())
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.GetById(chatId))
            .Returns(new Chat())
            .Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(true)
            .Verifiable();
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberToAddId))
            .Returns(true)
            .Verifiable();

        _chatsRepositoryMock.Setup(x => x.AddMember(chatId, memberToAddId)).Verifiable();

        // Act
        bool result = _messagesService.AddChatMember(chatId, memberId, memberToAddId);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberToAddId), Times.Once);
        Assert.IsFalse(result);
    }

    [Test]
    public void IsChatMember_ShouldReturnTrue()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(true)
            .Verifiable();
        
        // Act
        bool result = _messagesService.IsChatMember(chatId, memberId);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId));
        Assert.IsTrue(result);
    }
    
    [Test]
    public void IsChatMember_ShouldReturnFalse()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        
        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(false)
            .Verifiable();
        
        // Act
        bool result = _messagesService.IsChatMember(chatId, memberId);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId));
        Assert.IsFalse(result);
    }

    [Test]
    public void GetList_ShouldReturnMessagesList()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int start = 0;
        const int offset = 2;

        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(true)
            .Verifiable();
        
        _messagesRepositoryMock.Setup(x => x.GetList(chatId, start, offset))
            .Returns(new List<Message>
            {
                new()
                {
                    Id = 5,
                    ChatId = chatId,
                    SenderId = 7,
                    Text = "something",
                    SentAt = DateTime.UtcNow - TimeSpan.FromMinutes(5)
                },
                new()
                {
                    Id = 6,
                    ChatId = chatId,
                    SenderId = 25,
                    Text = "something",
                    SentAt = DateTime.UtcNow - TimeSpan.FromMinutes(3)
                }
            })
            .Verifiable();

        // Act
        IEnumerable<Message>? messages = _messagesService.GetList(chatId, memberId, start, offset);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId), Times.Once);
        _messagesRepositoryMock.Verify(x => x.GetList(chatId, start, offset), Times.Once);
        Assert.IsNotNull(messages);
    }
    
    [Test]
    public void GetList_IsNotJoined_ShouldReturnNull()
    {
        // Arrange
        const int chatId = 1;
        const int memberId = 1;
        const int start = 0;
        const int offset = 2;

        _chatsRepositoryMock.Setup(x => x.IsJoined(chatId, memberId))
            .Returns(false)
            .Verifiable();
        
        _messagesRepositoryMock.Setup(x => x.GetList(chatId, start, offset)).Verifiable();

        // Act
        IEnumerable<Message>? messages = _messagesService.GetList(chatId, memberId, start, offset);

        // Assert
        _chatsRepositoryMock.Verify(x => x.IsJoined(chatId, memberId), Times.Once);
        _messagesRepositoryMock.Verify(x => x.GetList(chatId, start, offset), Times.Never);
        Assert.IsNull(messages);
    }
}
