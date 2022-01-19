using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models;

public class GetRelationsDto : LimitsDto
{
    [Required]
    public string Nickname { get; set; } = string.Empty;
}
