using Friendbook.BusinessLogic;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class UserServiceTests
{
    private IUserService _userService = null!;
    private Mock<IUserRepository> _userRepository = null!;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        _userService = new UserService(_userRepository.Object, new UserValidator());
    }
    
    [Test]
    [TestCase("nickname1", "Nikolay", "Vasilyev", 2004, 3, 2)]
    [TestCase("nickname2", "Ivan", "Vdovin", 2003, 2, 16)]
    [TestCase("nickname3", "Gennadiy", "Usmanov", 1999, 6, 25)]
    public void Create_ShouldReturnTrue(string nickname, string firstName, string lastName, int yearOfBirth,
        int monthOfBirth, int dayOfBirth)
    {
        // Arrange
        User user = new User(nickname, firstName, lastName, yearOfBirth, monthOfBirth, dayOfBirth);
        
        _userRepository.Setup(x => x.Create(user)).Verifiable();

        // Act
        bool result = _userService.Create(user);

        // Assert
        _userRepository.Verify(x => x.Create(user), Times.Once());
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase("!!!", "Nikolay", "Vasilyev", 2004, 3, 2)]
    [TestCase("nick", "Nikolay", "Vasilyev", 2004, 3, 2)]
    [TestCase("nickname", "Ivan", "Vdovin", 2012, 2, 16)]
    [TestCase("nickname2", "G", "Usmanov", 1999, 6, 25)]
    [TestCase("nickname3", "Gennadiy", "U", 1999, 6, 25)]
    public void Create_ShouldReturnFalse(string nickname, string firstName, string lastName, int yearOfBirth,
        int monthOfBirth, int dayOfBirth)
    {
        // Arrange
        User user = new User(nickname, firstName, lastName, yearOfBirth, monthOfBirth, dayOfBirth);

        // Act
        bool result = _userService.Create(user);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GetById_ShouldReturnUser()
    {
        // Arrange
        const int userId = 1;

        _userRepository.Setup(x => x.GetById(userId)).Verifiable();
        
        // Act
        User? result = _userService.GetById(userId);

        // Assert
        _userRepository.Verify(x => x.GetById(userId), Times.Once);
    }

    [Test]
    public void GetByNickname_ShouldReturnUser()
    {
        // Arrange
        const string userNickname = "shuryak";

        _userRepository.Setup(x => x.GetByNickname(userNickname)).Verifiable();
        
        // Act
        User? result = _userService.GetByNickname(userNickname);

        // Assert
        _userRepository.Verify(x => x.GetByNickname(userNickname), Times.Once);
    }

    [Test]
    public void GetList_ShouldReturnUserList()
    {
        // Arrange
        _userRepository.Setup(x => x.GetList(0, 10));

        // Act
        IEnumerable<User> users = _userService.GetList(0, 10);

        // Assert
        _userRepository.Verify(x => x.GetList(0, 10), Times.Once);
    }
}
