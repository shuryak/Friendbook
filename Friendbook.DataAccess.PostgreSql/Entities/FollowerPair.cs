using System.ComponentModel.DataAnnotations;

namespace Friendbook.DataAccess.PostgreSql.Entities;

public class FollowerPair
{
    [Required]
    public int FollowerId { get; set; }
    [Required]
    public int FollowingId { get; set; }
    [Required]
    public bool IsRetroactive { get; set; } = false;
}
