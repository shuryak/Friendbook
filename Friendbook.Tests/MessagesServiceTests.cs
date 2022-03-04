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
    private Mock<IMessagesRepository> _messagesRepositoryMock = null!;
    private IMessagesService _messagesService = null!;

    [SetUp]
    public void SetUp()
    {
        _chatsRepositoryMock = new Mock<IChatsRepository>();
        _messagesRepositoryMock = new Mock<IMessagesRepository>();
        _messagesService = new MessagesService(_chatsRepositoryMock.Object, _messagesRepositoryMock.Object);
    }

    [Test]
    public void Send_ShouldReturnMessage()
    {
        // Arrange
        Message message = new Message(1, 1, "something");

        _messagesRepositoryMock.Setup(x => x.Create(message)).Verifiable();

        // Act
        Message? result = _messagesService.Send(message);

        // Assert
        _messagesRepositoryMock.Verify(x => x.Create(message), Times.Once);
        Assert.IsNotNull(result);
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
