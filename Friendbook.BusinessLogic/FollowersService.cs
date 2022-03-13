using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;

namespace Friendbook.BusinessLogic;

public class FollowersService : IFollowersService
{
    private readonly IFollowerPairRepository _followerPairRepository;
    private readonly IUserRepository _userRepository;

    public FollowersService(IFollowerPairRepository followerPairRepository, IUserRepository userRepository)
    {
        _followerPairRepository = followerPairRepository;
        _userRepository = userRepository;
    }

    public bool Follow(int followerId, int followingId)
    {
        if (followerId < 1 || followingId < 1) return false;

        RelationshipStatuses status = _followerPairRepository.GetRelationshipStatus(followerId, followingId);
        if (status is RelationshipStatuses.IsFollowed or RelationshipStatuses.IsFriends) return false;
        
        _followerPairRepository.Create(followerId, followingId);
        return true;
    }

    public RelationshipStatuses GetRelationshipStatus(int followerId, int followingId)
    {
        if (followerId < 1 || followingId < 1) return RelationshipStatuses.InvalidRelationship;
        
        return _followerPairRepository.GetRelationshipStatus(followerId, followingId);
    }

    public IEnumerable<User> GetFollowers(int userProfileId, int offset = 0, int limit = 10)
    {
        int[] followers = _followerPairRepository.GetFollowersIds(userProfileId, offset, limit);

        IEnumerable<User> followersList = _userRepository.GetManyByIds(followers);
        return followersList;
    }

    public IEnumerable<User> GetFollowings(int userProfileId, int offset = 0, int limit = 10)
    {
        int[] followers = _followerPairRepository.GetFollowingsIds(userProfileId, offset, limit);

        IEnumerable<User> followingsList = _userRepository.GetManyByIds(followers);
        return followingsList;
    }

    public IEnumerable<User> GetFriends(int userProfileId, int offset = 0, int limit = 10)
    {
        int[] followers = _followerPairRepository.GetFriendsIds(userProfileId, offset, limit);

        IEnumerable<User> friendsList = _userRepository.GetManyByIds(followers);
        return friendsList;
    }

    public bool Unfollow(int followerId, int followingId)
    {
        if (followerId < 1 || followingId < 1) return false;
        
        RelationshipStatuses status = _followerPairRepository.GetRelationshipStatus(followerId, followingId);
        if (status is RelationshipStatuses.IsNotFollowed or RelationshipStatuses.IsFollowing) return false;
        
        _followerPairRepository.Delete(followerId, followingId);
        return true;
    }
}
