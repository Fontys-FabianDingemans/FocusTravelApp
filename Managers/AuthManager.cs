namespace FocusTravelApp.Managers;

public class AuthManager
{
    
    private string _token;

    public AuthManager()
    {
        _token = AppSettings.UserToken;
    }
    
    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(_token);
    }
    
    public string GetToken()
    {
        return _token;
    }

    public bool Login(string username, string password)
    {
        //TODO: Call API to get token
        
        AppSettings.UserToken = "temp";
        return true;
    }

    public bool Register(string username, string password)
    {
        throw new NotImplementedException();
    }
    
    public void Logout()
    {
        _token = string.Empty;
        AppSettings.UserToken = string.Empty;
    }
}