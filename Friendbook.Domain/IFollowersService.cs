using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IFollowersService
{
    public bool Follow(int followerId, int followingId);
    public List<UserProfile> GetFollowers(int userProfileId, int start = 0, int offset = 10);
    public List<UserProfile> GetFollowings(int userProfileId, int start = 0, int offset = 10);
    public List<UserProfile> GetFriends(int userProfileId, int start = 0, int offset = 10);
    public bool Unfollow(int followerId, int followingId);
}
