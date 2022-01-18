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

    [Test]
    public void GetById_ShouldReturnUserProfile()
    {
        // Arrange
        const int userProfileId = 1;

        _userProfileRepository.Setup(x => x.GetById(userProfileId)).Verifiable();
        
        // Act
        UserProfile result = _userProfileService.GetById(userProfileId);

        // Assert
        _userProfileRepository.Verify(x => x.GetById(userProfileId), Times.Once);
    }

    [Test]
    public void GetByNickname_ShouldReturnUserProfile()
    {
        // Arrange
        const string userProfileNickname = "shuryak";

        _userProfileRepository.Setup(x => x.GetByNickname(userProfileNickname)).Verifiable();
        
        // Act
        UserProfile result = _userProfileService.GetByNickname(userProfileNickname);

        // Assert
        _userProfileRepository.Verify(x => x.GetByNickname(userProfileNickname), Times.Once);
    }

    [Test]
    public void GetList_ShouldReturnUserProfileList()
    {
        // Arrange
        _userProfileRepository.Setup(x => x.GetList(0, 10));

        // Act
        IEnumerable<UserProfile> userProfiles = _userProfileService.GetList(0, 10);

        // Assert
        _userProfileRepository.Verify(x => x.GetList(0, 10), Times.Once);
    }
}
