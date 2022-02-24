using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IFollowersService
{
    public bool Follow(int followerId, int followingId);
    public RelationshipStatuses GetRelationshipStatus(int followerId, int followingId);
    public IEnumerable<UserProfile> GetFollowers(int userProfileId, int start = 0, int offset = 10);
    public IEnumerable<UserProfile> GetFollowings(int userProfileId, int start = 0, int offset = 10);
    public IEnumerable<UserProfile> GetFriends(int userProfileId, int start = 0, int offset = 10);
    public bool Unfollow(int followerId, int followingId);
}
