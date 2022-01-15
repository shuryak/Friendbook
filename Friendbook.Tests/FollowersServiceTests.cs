using Friendbook.BusinessLogic;
using Friendbook.Domain;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class FollowersServiceTests
{
    private Mock<IFollowerPairRepository> _followerPairRepository;
    private Mock<IUserProfileRepository> _userProfileRepository;
    private IFollowersService _followersService;

    [SetUp]
    public void SetUp()
    {
        _followerPairRepository = new Mock<IFollowerPairRepository>();
        _userProfileRepository = new Mock<IUserProfileRepository>();
    }

    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Follow_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepository
            .Setup(x => x.Create(followerId, followingId))
            .Verifiable();
        
        // Act
        bool result = _followersService.Follow(followerId, followingId);

        // Assert
        _followerPairRepository.Verify(x => x.Create(followerId, followingId), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    public void Follow_ShouldReturnFalse(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepository.Setup(x => x.Create(followerId, followingId)).Verifiable();
        
        // Act
        bool result = _followersService.Follow(followerId, followingId);

        //Assert
        _followerPairRepository.Verify(x => x.Create(followerId, followingId), Times.Never);
        Assert.IsFalse(result);
    }

    [Test]
    public void Follow_TwiceToOneUserProfile_ShouldReturnFalse()
    {
        // Arrange

        // Act
        bool result1 = _followersService.Follow(1, 2);
        bool result2 = _followersService.Follow(1, 2);

        // Assert
        Assert.IsTrue(result1 != result2 && result2 == false);
    }
}
