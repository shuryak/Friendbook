using Friendbook.BusinessLogic;
using Friendbook.Domain;
using Friendbook.Domain.Models;
using Moq;
using NUnit.Framework;

namespace Friendbook.Tests;

public class FollowersServiceTests
{
    private Mock<IFollowerPairRepository> _followerPairRepositoryMock;
    private Mock<IUserRepository> _userProfileRepositoryMock;
    private IFollowersService _followersService;

    [SetUp]
    public void SetUp()
    {
        _followerPairRepositoryMock = new Mock<IFollowerPairRepository>();
        _userProfileRepositoryMock = new Mock<IUserRepository>();
        _followersService = new FollowersService(_followerPairRepositoryMock.Object, _userProfileRepositoryMock.Object);
    }

    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Follow_IsNotFollowed_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock
            .Setup(x => x.Create(followerId, followingId))
            .Verifiable();

        _followerPairRepositoryMock
            .Setup(x => x.GetRelationshipStatus(followerId, followingId))
            .Returns(RelationshipStatuses.IsNotFollowed);

        // Act
        bool result = _followersService.Follow(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.Create(followerId, followingId), Times.Once);
        Assert.IsTrue(result);
    }
    
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Follow_IsFollowing_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock
            .Setup(x => x.Create(followerId, followingId))
            .Verifiable();

        _followerPairRepositoryMock
            .Setup(x => x.GetRelationshipStatus(followerId, followingId))
            .Returns(RelationshipStatuses.IsFollowing);

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
    [TestCase(1, 2)]
    public void GetRelationshipStatus_ShouldReturnValidRelationship(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock.Setup(x => x.GetRelationshipStatus(followerId, followingId)).Verifiable();

        // Act
        RelationshipStatuses result = _followersService.GetRelationshipStatus(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.GetRelationshipStatus(followerId, followingId), Times.Once);
    }
    
    [Test]
    [TestCase(1, 0)]
    [TestCase(-5, 2)]
    [TestCase(0, 0)]
    [TestCase(-5, -5)]
    public void GetRelationshipStatus_ShouldReturnInvalidRelationship(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock.Setup(x => x.GetRelationshipStatus(followerId, followingId)).Verifiable();

        // Act
        RelationshipStatuses result = _followersService.GetRelationshipStatus(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.GetRelationshipStatus(followerId, followingId), Times.Never);
        Assert.IsTrue(result is RelationshipStatuses.InvalidRelationship);
    }
    
    [Test]
    public void GetFollowers_ShouldReturnFollowersList()
    {
        // Arrange
        const int userProfileId = 1;

        _followerPairRepositoryMock
            .Setup(x => x.GetFollowersIds(userProfileId, 0, 10))
            .Returns(() => new[] {2, 5, 3})
            .Verifiable();
        
        _userProfileRepositoryMock
            .Setup(x => x.GetManyByIds(new[] {2, 5, 3}))
            .Verifiable();

        // Act
        IEnumerable<User> result = _followersService.GetFollowers(userProfileId);

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
            .Setup(x => x.GetFollowingsIds(userProfileId, 0, 10))
            .Returns(() => new[] {2, 5, 3})
            .Verifiable();

        _userProfileRepositoryMock
            .Setup(x => x.GetManyByIds(new[] {2, 5, 3}))
            .Verifiable();
        
        // Act
        IEnumerable<User> result = _followersService.GetFollowings(userProfileId);

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
            .Setup(x => x.GetFriendsIds(userProfileId, 0, 10))
            .Returns(() => new[] {2, 5, 3})
            .Verifiable();

        _userProfileRepositoryMock
            .Setup(x => x.GetManyByIds(new[] {2, 5, 3}))
            .Verifiable();
        
        // Act
        IEnumerable<User> result = _followersService.GetFriends(userProfileId);

        // Assert
        _followerPairRepositoryMock.VerifyAll();
        _userProfileRepositoryMock.VerifyAll();
    }
    
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Unfollow_IsFollowed_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock
            .Setup(x => x.Delete(followerId, followingId))
            .Verifiable();

        _followerPairRepositoryMock
            .Setup(x => x.GetRelationshipStatus(followerId, followingId))
            .Returns(RelationshipStatuses.IsFollowed);

        // Act
        bool result = _followersService.Unfollow(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.Delete(followerId, followingId), Times.Once);
        Assert.IsTrue(result);
    }
    
    [Test]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    [TestCase(3, 2)]
    public void Unfollow_IsFriends_ShouldReturnTrue(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock
            .Setup(x => x.Delete(followerId, followingId))
            .Verifiable();

        _followerPairRepositoryMock
            .Setup(x => x.GetRelationshipStatus(followerId, followingId))
            .Returns(RelationshipStatuses.IsFriends);

        // Act
        bool result = _followersService.Unfollow(followerId, followingId);

        // Assert
        _followerPairRepositoryMock.Verify(x => x.Delete(followerId, followingId), Times.Once);
        Assert.IsTrue(result);
    }

    [Test]
    public void Unfollow_IsNotFollowed_ShouldReturnFalse()
    {
        // Arrange
        _followerPairRepositoryMock.Setup(x => x.Delete(1, 2)).Verifiable();
        _followerPairRepositoryMock
            .Setup(x => x.GetRelationshipStatus(1, 2))
            .Returns(RelationshipStatuses.IsNotFollowed);
        
        // Act
        bool result = _followersService.Unfollow(1, 2);

        //Assert
        // _followerPairRepositoryMock.Verify(x => x.Delete(1, 2), Times.Never);
        Assert.IsFalse(result);
    }

    [Test]
    [TestCase(-1, 1)]
    [TestCase(1, -1)]
    [TestCase(1, 0)]
    public void Unfollow_InvalidArguments_ShouldReturnFalse(int followerId, int followingId)
    {
        // Arrange
        _followerPairRepositoryMock.Setup(x => x.Delete(followerId, followingId)).Verifiable();

        // Act
        bool result = _followersService.Unfollow(followerId, followingId);

        //Assert
        _followerPairRepositoryMock.Verify(x => x.Delete(followerId, followingId), Times.Never);
        Assert.IsFalse(result);
    }
    
    [Test]
    public void Unfollow_TwiceFromOneUserProfile_ShouldReturnFalse()
    {
        // Arrange
        _followerPairRepositoryMock
            .SetupSequence(x => x.GetRelationshipStatus(1, 2))
            .Returns(RelationshipStatuses.IsFollowed)
            .Returns(RelationshipStatuses.IsNotFollowed);
        
        _followerPairRepositoryMock
            .SetupSequence(x => x.GetRelationshipStatus(2, 3))
            .Returns(RelationshipStatuses.IsFriends)
            .Returns(RelationshipStatuses.IsFollowing);

        // Act
        bool result1 = _followersService.Unfollow(1, 2);
        bool result2 = _followersService.Unfollow(1, 2);
        bool result3 = _followersService.Unfollow(2, 3);
        bool result4 = _followersService.Unfollow(2, 3);
        
        // Assert
        Assert.IsTrue(result1 != result2 && result2 == false);
        Assert.IsTrue(result3 != result4 && result4 == false);
    }
}
