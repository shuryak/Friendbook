using Friendbook.BusinessLogic;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class UserProfileServiceTests
{
    private IUserProfileService _userProfileService;
    private Mock<IUserProfileRepository> _userProfileRepository;

    [SetUp]
    public void SetUp()
    {
        _userProfileRepository = new Mock<IUserProfileRepository>();
        _userProfileService = new UserProfileService(_userProfileRepository.Object, new UserProfileValidator());
    }
    
    [Test]
    [TestCase("nickname1", "Nikolay", "Vasilyev", 2004, 3, 2)]
    [TestCase("nickname2", "Ivan", "Vdovin", 2003, 2, 16)]
    [TestCase("nickname3", "Gennadiy", "Usmanov", 1999, 6, 25)]
    public void Create_ShouldReturnTrue(string nickname, string firstName, string lastName, int yearOfBirth,
        int monthOfBirth, int dayOfBirth)
    {
        // Arrange
        UserProfile userProfile = new UserProfile(nickname, firstName, lastName, yearOfBirth, monthOfBirth, dayOfBirth);
        
        _userProfileRepository.Setup(x => x.Create(userProfile)).Verifiable();

        // Act
        bool result = _userProfileService.Create(userProfile);

        // Assert
        _userProfileRepository.Verify(x => x.Create(userProfile), Times.Once());
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
        UserProfile userProfile = new UserProfile(nickname, firstName, lastName, yearOfBirth, monthOfBirth, dayOfBirth);

        // Act
        bool result = _userProfileService.Create(userProfile);

        // Assert
        Assert.IsFalse(result);
    }
}
