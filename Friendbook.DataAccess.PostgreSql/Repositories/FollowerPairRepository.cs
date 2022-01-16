using AutoMapper;
using Friendbook.DataAccess.PostgreSql.Entities;
using Friendbook.Domain;

namespace Friendbook.DataAccess.PostgreSql.Repositories;

public class FollowerPairRepository : IFollowerPairRepository
{
    private readonly FriendbookDbContext _dbContext;
    private readonly IMapper _mapper;

    public FollowerPairRepository(FriendbookDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public void Create(int followerId, int followingId)
    {
        FollowerPair followerPair = new()
        {
            FollowerId = followerId,
            FollowingId = followingId,
            IsRetroactive = false
        };

        FollowerPair? existingFollowerPair = _dbContext.FollowerPairs
            .FirstOrDefault(x => 
                (x.FollowerId == followerId && x.FollowingId == followingId) ||
                (x.FollowerId == followingId && x.FollowingId == followerId)
                );

        if (existingFollowerPair != null && existingFollowerPair.IsRetroactive) return;
        if (existingFollowerPair != null && existingFollowerPair.FollowerId == followerPair.FollowerId) return;

        if (existingFollowerPair == null)
        {
            _dbContext.FollowerPairs.Add(followerPair);
        }
        else
        {
            existingFollowerPair.IsRetroactive = true;
            _dbContext.Update(existingFollowerPair);
        }

        _dbContext.SaveChanges();
    }

    public RelationshipStatuses GetRelationshipStatus(int followerId, int followingId)
    {
        FollowerPair? followerPair = _dbContext.FollowerPairs
            .FirstOrDefault(x => 
                (x.FollowerId == followerId && x.FollowingId == followingId) ||
                (x.FollowerId == followingId && x.FollowingId == followerId)
            );
        
        if (followerPair == null) return RelationshipStatuses.IsNotFollowed;

        if (followerPair.IsRetroactive) return RelationshipStatuses.IsFriends;

        return followerPair.FollowerId == followerId
            ? RelationshipStatuses.IsFollowed
            : RelationshipStatuses.IsFollowing;
    }

    public int[] GetFollowersIds(int userId)
    {
        int[] followersIds = _dbContext.FollowerPairs
            .Where(x => x.FollowingId == userId && x.IsRetroactive == false)
            .Select(x => x.FollowerId)
            .ToArray();

        return followersIds;
    }

    public int[] GetFollowingsIds(int userId)
    {
        int[] followingsIds = _dbContext.FollowerPairs
            .Where(x => x.FollowerId == userId && x.IsRetroactive == false)
            .Select(x => x.FollowingId)
            .ToArray();

        return followingsIds;
    }

    public int[] GetFriendsIds(int userId)
    {
        int[] firstFriendsIds = _dbContext.FollowerPairs
            .Where(x => (x.FollowerId == userId || x.FollowingId == userId) && x.IsRetroactive == true)
            .Select(x => x.FollowerId)
            .ToArray()
            .Where(x => x != userId)
            .ToArray();
        
        int[] secondFriendsIds = _dbContext.FollowerPairs
            .Where(x => (x.FollowerId == userId || x.FollowingId == userId) && x.IsRetroactive == true)
            .Select(x => x.FollowingId)
            .ToArray()
            .Where(x => x != userId)
            .ToArray();

        return firstFriendsIds.Concat(secondFriendsIds).ToArray();
    }

    public void Delete(int followerId, int followingId)
    {
        FollowerPair? existingFollowerPair = _dbContext.FollowerPairs
            .FirstOrDefault(x => 
                (x.FollowerId == followerId && x.FollowingId == followingId) ||
                (x.FollowerId == followingId && x.FollowingId == followerId)
                );

        if (existingFollowerPair == null) return;
        if (existingFollowerPair.FollowingId == followerId && existingFollowerPair.IsRetroactive == false) return;
        
        if (existingFollowerPair.FollowerId == followerId && existingFollowerPair.IsRetroactive == false)
        {
            _dbContext.FollowerPairs.Remove(existingFollowerPair);
        }
        else if (existingFollowerPair.FollowerId == followerId && existingFollowerPair.IsRetroactive)
        {
            _dbContext.FollowerPairs.Remove(existingFollowerPair);
            _dbContext.FollowerPairs.Add(new FollowerPair
            {
                FollowerId = followingId,
                FollowingId = followerId,
                IsRetroactive = false
            });
        }
        else if (existingFollowerPair.FollowingId == followerId && existingFollowerPair.IsRetroactive)
        {
            existingFollowerPair.IsRetroactive = false;
            _dbContext.FollowerPairs.Update(existingFollowerPair);
        }

        _dbContext.SaveChanges();
    }
}
