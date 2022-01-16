using Friendbook.BusinessLogic;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class FollowersServiceTests
{
    private Mock<IFollowerPairRepository> _followerPairRepositoryMock;
    private Mock<IUserProfileRepository> _userProfileRepositoryMock;
    private IFollowersService _followersService;

    [SetUp]
    public void SetUp()
    {
        _followerPairRepositoryMock = new Mock<IFollowerPairRepository>();
        _userProfileRepositoryMock = new Mock<IUserProfileRepository>();
        _followersService = new FollowersService(_followerPairRepositoryMock.Object, _userProfileRepositoryMock.Object);
    }

    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Follow_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock
            .Setup(x => x.Create(followerId, followingId))
            .Verifiable();
        
        // Act
        bool result = _followersService.Follow(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.Create(followerId, followingId), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(1, 0)]
    public void Follow_ShouldReturnFalse(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock.Setup(x => x.Create(followerId, followingId)).Verifiable();
        
        // Act
        bool result = _followersService.Follow(followerId, followingId);

        //Assert
        _followerPairRepositoryMock.Verify(x => x.Create(followerId, followingId), Times.Never);
        Assert.IsFalse(result);
    }

    [Test]
    public void Follow_TwiceToOneUserProfile_ShouldReturnFalse()
    {
        // Arrange
        _followerPairRepositoryMock
            .SetupSequence(x => x.GetRelationshipStatus(1, 2))
            .Returns(RelationshipStatuses.IsNotFollowed)
            .Returns(RelationshipStatuses.IsFollowed);

        // Act
        bool result1 = _followersService.Follow(1, 2);
        bool result2 = _followersService.Follow(1, 2);

        // Assert
        Assert.IsTrue(result1 != result2 && result2 == false);
    }
    
    [Test]
    public void GetFollowers_ShouldReturnFollowersList()
    {
        // Arrange
        const int userProfileId = 1;

        _followerPairRepositoryMock
            .Setup(x => x.GetFollowersIds(userProfileId))
            .Returns(() => new[] {2, 5, 3})
            .Verifiable();
        
        _userProfileRepositoryMock
            .Setup(x => x.GetManyByIds(new[] {2, 5, 3}))
            .Verifiable();

        // Act
        IEnumerable<UserProfile> result = _followersService.GetFollowers(userProfileId);

        // Assert
        _followerPairRepositoryMock.VerifyAll();
        _userProfileRepositoryMock.VerifyAll();
    }

    [Test]
    public void GetFollowings_ShouldReturnFollowingsList()
    {
        // Arrange
        const int userProfileId = 1;
        
        _followerPairRepositoryMock
            .Setup(x => x.GetFollowingsIds(userProfileId))
            .Returns(() => new[] {2, 5, 3})
            .Verifiable();

        _userProfileRepositoryMock
            .Setup(x => x.GetManyByIds(new[] {2, 5, 3}))
            .Verifiable();
        
        // Act
        IEnumerable<UserProfile> result = _followersService.GetFollowings(userProfileId);

        // Assert
        _followerPairRepositoryMock.VerifyAll();
        _userProfileRepositoryMock.VerifyAll();
    }

    [Test]
    public void GetFriends_ShouldReturnFriendsList()
    {
        // Arrange
        const int userProfileId = 1;
        
        _followerPairRepositoryMock
            .Setup(x => x.GetFriendsIds(userProfileId))
            .Returns(() => new[] {2, 5, 3})
            .Verifiable();

        _userProfileRepositoryMock
            .Setup(x => x.GetManyByIds(new[] {2, 5, 3}))
            .Verifiable();
        
        // Act
        IEnumerable<UserProfile> result = _followersService.GetFriends(userProfileId);

        // Assert
        _followerPairRepositoryMock.VerifyAll();
        _userProfileRepositoryMock.VerifyAll();
    }
    
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Unfollow_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock
            .Setup(x => x.Delete(followerId, followingId))
            .Verifiable();
        
        // Act
        bool result = _followersService.Unfollow(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.Delete(followerId, followingId), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(1, 0)]
    public void Unfollow_ShouldReturnFalse(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock.Setup(x => x.Delete(followerId, followingId)).Verifiable();
        
        // Act
        bool result = _followersService.Unfollow(followerId, followingId);

        //Assert
        _followerPairRepositoryMock.Verify(x => x.Delete(followerId, followingId), Times.Never);
        Assert.IsFalse(result);
    }
}
