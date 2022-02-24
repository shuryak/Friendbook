using Friendbook.Domain;
using Friendbook.Domain.Models;
using Friendbook.Domain.RepositoryAbstractions;
using Friendbook.Domain.ServiceAbstractions;

namespace Friendbook.BusinessLogic;

public class FollowersService : IFollowersService
{
    private readonly IFollowerPairRepository _followerPairRepository;
    private readonly IUserProfileRepository _userProfileRepository;

    public FollowersService(IFollowerPairRepository followerPairRepository, IUserProfileRepository userProfileRepository)
    {
        _followerPairRepository = followerPairRepository;
        _userProfileRepository = userProfileRepository;
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

    public IEnumerable<UserProfile> GetFollowers(int userProfileId, int offset = 0, int limit = 10)
    {
        int[] followers = _followerPairRepository.GetFollowersIds(userProfileId, offset, limit);

        IEnumerable<UserProfile> followersList = _userProfileRepository.GetManyByIds(followers);
        return followersList;
    }

    public IEnumerable<UserProfile> GetFollowings(int userProfileId, int offset = 0, int limit = 10)
    {
        int[] followers = _followerPairRepository.GetFollowingsIds(userProfileId, offset, limit);

        IEnumerable<UserProfile> followingsList = _userProfileRepository.GetManyByIds(followers);
        return followingsList;
    }

    public IEnumerable<UserProfile> GetFriends(int userProfileId, int offset = 0, int limit = 10)
    {
        int[] followers = _followerPairRepository.GetFriendsIds(userProfileId, offset, limit);

        IEnumerable<UserProfile> friendsList = _userProfileRepository.GetManyByIds(followers);
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
