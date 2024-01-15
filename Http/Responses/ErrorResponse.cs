namespace FocusTravelApp.Http.Responses;

public class ErrorResponse
{
    private string Error;
    
    public ErrorResponse(string error)
    {
        Error = error;
    }
    
    public string GetMessage()
    {
        return Error;
    }
    
}