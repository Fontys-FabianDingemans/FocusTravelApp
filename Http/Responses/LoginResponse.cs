using Newtonsoft.Json;

namespace FocusTravelApp.Http.Responses;

public class LoginResponse
{
    private string Token;
    
    public LoginResponse(string token)
    {
        Token = token;
    }
    
    public string GetToken()
    {
        return Token;
    }
    
}