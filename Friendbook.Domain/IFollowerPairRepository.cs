using Friendbook.Domain.Models;

namespace Friendbook.Domain;

public interface IFollowerPairRepository
{
    void Create(int followerId, int followingId);
    int[] GetFollowersIds(int userId);
    int[] GetFollowingsIds(int userId);
    int[] GetFriendsIds(int userId);
    void Delete(int followerId, int followingId);
}
