using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models.Messages;

public class CreateChatMemberDto
{
    [Required]
    public int ChatId { get; set; }
    
    [Required]
    public int MemberId { get; set; }
}
