namespace Friendbook.Api.Models.RealTime;

public class SuccessfulRequest
{
    public SuccessfulRequest(string result)
    {
        Result = result;
    }
    
    public string Result { get; set; }
}
