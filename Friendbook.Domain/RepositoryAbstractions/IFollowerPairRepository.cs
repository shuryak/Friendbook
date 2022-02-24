namespace Friendbook.Domain;

public interface IFollowerPairRepository
{
    void Create(int followerId, int followingId);
    RelationshipStatuses GetRelationshipStatus(int followerId, int followingId);
    int[] GetFollowersIds(int userId, int offset, int limit);
    int[] GetFollowingsIds(int userId, int offset, int limit);
    int[] GetFriendsIds(int userId, int offset, int limit);
    void Delete(int followerId, int followingId);
}
