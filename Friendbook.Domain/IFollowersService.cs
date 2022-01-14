using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IFollowersService
{
    public bool Follow(FollowerPair followerPair);
    public List<UserProfile> GetFollowers(int userProfileId, int start, int offset);
    public List<UserProfile> GetFollowings(int userProfileId, int start, int offset);
    public List<UserProfile> GetFriends(int userProfileId, int start, int offset);
}
