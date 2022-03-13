namespace Friendbook.Api.Models.RealTime;

public class InvalidRequest
{
    public InvalidRequest(string error)
    {
        Error = error;
    }
    
    public string Error { get; set; }
}
