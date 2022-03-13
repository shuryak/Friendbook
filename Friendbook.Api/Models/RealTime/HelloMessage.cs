namespace Friendbook.Api.Models.RealTime;

public class HelloMessage
{
    public HelloMessage(string connectionId)
    {
        ConnectionId = connectionId;
    }
    
    public string ConnectionId { get; set; }
}
