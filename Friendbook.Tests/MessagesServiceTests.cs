using Friendbook.BusinessLogic;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class MessagesServiceTests
{
    private Mock<IChatsRepository> _chatsRepositoryMock;
    private Mock<IMessagesRepository> _messagesRepositoryMock;
    private IMessagesService _messagesService;

    [SetUp]
    public void SetUp()
    {
        _chatsRepositoryMock = new Mock<IChatsRepository>();
        _messagesRepositoryMock = new Mock<IMessagesRepository>();
        _messagesService = new MessagesService(_chatsRepositoryMock.Object, _messagesRepositoryMock.Object);
    }

    [Test]
    public void Send_ShouldReturnTrue()
    {
        // Arrange
        Message message = new Message
        {
            SenderId = 1,
            ChatId = 1,
            Text = "Something",
            SentAt = DateTime.Now
        };

        _messagesRepositoryMock.Setup(x => x.Create(message)).Verifiable();

        // Act
        bool result = _messagesService.Send(message);

        // Assert
        _messagesRepositoryMock.Verify(x => x.Create(message), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    public void GetList_ShouldReturnMessagesList()
    {
        // Arrange
        _messagesRepositoryMock.Setup(x => x.GetList(0, 10));

        // Act
        IEnumerable<Message> messages = _messagesService.GetList(0, 10);

        // Assert
        _messagesRepositoryMock.Verify(x => x.GetList(0, 10), Times.Once);
    }
}
