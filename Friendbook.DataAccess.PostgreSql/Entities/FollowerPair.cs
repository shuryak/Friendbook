using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities;

public class FollowerPair
{
    [Required]
    public int FollowerId { get; set; }
    [Required]
    public int FollowingId { get; set; }

    public bool IsRetroactive { get; set; } = false;
}
