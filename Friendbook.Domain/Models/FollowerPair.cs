namespace Friendbook.Domain.Models;

public class FollowerPair
{
    public FollowerPair(int followerId, int followingId, bool isRetroactive = false)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        IsRetroactive = isRetroactive;
    }
    
    public int FollowerId { get; set; }

    public int FollowingId { get; set; }

    public bool IsRetroactive { get; set; } = false;
}
