using Friendbook.Domain.Models;

namespace Friendbook.Domain.ServiceAbstractions;

public interface IFollowersService
{
    public bool Follow(int followerId, int followingId);
    public RelationshipStatuses GetRelationshipStatus(int followerId, int followingId);
    public IEnumerable<User> GetFollowers(int userProfileId, int start = 0, int offset = 10);
    public IEnumerable<User> GetFollowings(int userProfileId, int start = 0, int offset = 10);
    public IEnumerable<User> GetFriends(int userProfileId, int start = 0, int offset = 10);
    public bool Unfollow(int followerId, int followingId);
}
