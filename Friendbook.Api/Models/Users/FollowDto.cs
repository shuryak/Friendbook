using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models.Users;

public class FollowDto
{
    [Required]
    public int FollowerId { get; set; }
    [Required]
    public int FollowingId { get; set; }
}
