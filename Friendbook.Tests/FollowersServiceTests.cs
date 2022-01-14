using Friendbook.Domain;
using Friendbook.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class FollowersServiceTests
{
    private Mock<IRepository<FollowerPair>> _followerPairRepository;
    private IFollowersService _followersService;

    [SetUp]
    public void SetUp()
    {
        _followerPairRepository = new Mock<IRepository<FollowerPair>>();
    }

    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Follow_ShouldReturnTrue(int followerUserProfileId, int followingUserProfileId)
    {
        // Arrange
        FollowerPair followerPair = new(followerUserProfileId, followingUserProfileId);

        _followerPairRepository.Setup(x => x.Create(followerPair)).Verifiable();
        
        // Act
        bool result = _followersService.Follow(followerPair);

        // Assert
        _followerPairRepository.Verify(x => x.Create(followerPair), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase(1, 1)]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    public void Follow_ShouldReturnFalse(int followerUserProfileId, int followingUserProfileId)
    {
        // Arrange
        FollowerPair followerPair = new(followerUserProfileId, followingUserProfileId);

        _followerPairRepository.Setup(x => x.Create(followerPair)).Verifiable();
        
        // Act
        bool result = _followersService.Follow(followerPair);

        //Assert
        _followerPairRepository.Verify(x => x.Create(followerPair), Times.Never);
        Assert.IsFalse(result);
    }

    [Test]
    public void Follow_TwiceToOneUserProfile_ShouldReturnFalse()
    {
        // Arrange
        FollowerPair followerPair = new(1, 2);

        // Act
        bool result1 = _followersService.Follow(followerPair);
        bool result2 = _followersService.Follow(followerPair);

        // Assert
        Assert.IsTrue(result1 != result2 && result2 == false);
    }
}
