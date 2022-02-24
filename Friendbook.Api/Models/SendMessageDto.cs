namespace Friendbook.Api.Models;

public class SendMessageDto
{
    public int ChatId { get; set; }

    public string Text { get; set; } = string.Empty;
}
