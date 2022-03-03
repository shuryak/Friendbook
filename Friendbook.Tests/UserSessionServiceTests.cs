using Friendbook.BusinessLogic;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class UserSessionServiceTests
{
    private IUserSessionService _userSessionService = null!;
    private Mock<IUserSessionRepository> _userSessionRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _userSessionRepository = new Mock<IUserSessionRepository>();
        _userSessionService = new UserSessionService(_userSessionRepository.Object);
    }

    [Test]
    public void Create_ShouldReturnUserSession()
    {
        // Arrange
        const int userId = 1;
        
        User user = new User("shuryak", "Alexander", "Konovalov", 2004, 1, 24)
        {
            Id = userId
        };

        TimeSpan expiresIn = TimeSpan.FromMinutes(30);
        
        _userSessionRepository
            .Setup(x => x.Create(user, expiresIn))
            .Returns(new UserSession
            {
                SessionId = 1,
                UserId = userId,
                RefreshToken = "random-string",
                ExpiresAt = DateTime.UtcNow + TimeSpan.FromMinutes(30)
            })
            .Verifiable();

        // Act
        UserSession userSession = _userSessionService.Create(user, expiresIn);

        // Assert
        _userSessionRepository.Verify(x => x.Create(user, expiresIn), Times.Once());
        Assert.IsNotNull(userSession);
    }

    [Test]
    public void GetById_ShouldReturnUserSession()
    {
        // Arrange
        const int sessionId = 1;
        
        _userSessionRepository
            .Setup(x => x.GetById(sessionId))
            .Returns(new UserSession
            {
                SessionId = sessionId,
                UserId = 1,
                RefreshToken = "random-string",
                ExpiresAt = DateTime.UtcNow + TimeSpan.FromMinutes(30)
            })
            .Verifiable();

        // Act
        UserSession? userSession = _userSessionService.GetById(sessionId);

        // Assert
        _userSessionRepository.Verify(x => x.GetById(sessionId), Times.Once());
        Assert.IsNotNull(userSession);
        Assert.IsTrue(userSession!.SessionId == sessionId);
    }

    [Test]
    public void GetById_NotExists_ShouldReturnNull()
    {
        // Arrange
        const int sessionId = 1;
        
        _userSessionRepository
            .Setup(x => x.GetById(1))
            .Returns((UserSession?)null)
            .Verifiable();
        
        // Act
        UserSession? userSession = _userSessionService.GetById(sessionId);
        
        // Assert
        Assert.IsNull(userSession);
    }
    
    [Test]
    public void GetById_Expires_ShouldReturnNull()
    {
        // Arrange
        const int sessionId = 1;
        
        _userSessionRepository
            .Setup(x => x.GetById(sessionId))
            .Returns(new UserSession
            {
                SessionId = sessionId,
                UserId = 1,
                RefreshToken = "random-string",
                ExpiresAt = DateTime.UtcNow - TimeSpan.FromDays(1) // expires
            })
            .Verifiable();
        
        // Act
        UserSession? userSession = _userSessionService.GetById(sessionId);
        
        // Assert
        _userSessionRepository.Verify(x => x.GetById(sessionId), Times.Once());
        Assert.IsNull(userSession);
    }

    [Test]
    public void Renew_ShouldReturnUserSession()
    {
        // Arrange
        const int sessionId = 1;
        const string refreshToken = "random-string";
        TimeSpan expiresIn = TimeSpan.FromMinutes(30);
        
        _userSessionRepository
            .Setup(x => x.Update(refreshToken, expiresIn))
            .Returns(new UserSession
            {
                SessionId = sessionId,
                UserId = 1,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow + expiresIn
            })
            .Verifiable();

        // Act
        UserSession? userSession = _userSessionService.Renew(refreshToken, expiresIn);

        // Assert
        _userSessionRepository.Verify(x => x.Update(refreshToken, expiresIn), Times.Once());
        Assert.IsNotNull(userSession);
    }
    
    [Test]
    public void Renew_Expires_ShouldReturnNull()
    {
        // Arrange
        const int sessionId = 1;
        const string refreshToken = "random-string";
        TimeSpan expiresIn = TimeSpan.FromMinutes(30);
        
        _userSessionRepository
            .Setup(x => x.Update(refreshToken, expiresIn))
            .Returns(new UserSession
            {
                SessionId = sessionId,
                UserId = 1,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow - TimeSpan.FromDays(1) // expires
            })
            .Verifiable();

        // Act
        UserSession? userSession = _userSessionService.Renew(refreshToken, expiresIn);

        // Assert
        _userSessionRepository.Verify(x => x.Update(refreshToken, expiresIn), Times.Once());
        Assert.IsNull(userSession);
    }

    [Test]
    public void Renew_NotExists_ShouldReturnNull()
    {
        // Arrange
        const string refreshToken = "random-string";
        TimeSpan expiresIn = TimeSpan.FromMinutes(30);
        
        _userSessionRepository
            .Setup(x => x.Update(refreshToken, expiresIn))
            .Returns((UserSession?)null)
            .Verifiable();
        
        // Act
        UserSession? userSession = _userSessionService.Renew(refreshToken, expiresIn);
        
        // Assert
        Assert.IsNull(userSession);
    }
}
