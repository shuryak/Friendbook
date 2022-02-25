using System.ComponentModel.DataAnnotations;

namespace Friendbook.Api.Models.Messages;

public class SendMessageDto
{
    [Required]
    public int ChatId { get; set; }

    [Required]
    public string Text { get; set; } = string.Empty;
}
