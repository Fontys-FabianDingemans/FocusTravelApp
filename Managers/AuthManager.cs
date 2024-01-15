using FocusTravelApp.Http.Responses;

namespace FocusTravelApp.Managers;

using Http.Tasks;

public class AuthManager
{
    private string _accountToken;

    public AuthManager()
    {
        _accountToken = AppSettings.UserToken;
    }

    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(_accountToken);
    }

    public string GetToken()
    {
        return _accountToken;
    }

    public bool Login(string email, string password)
    {
        //TODO: Call API to login
        LoginTask.Run(email, password, (LoginResponse response) =>
        {
            
        });
        
        SaveToken("temp");
        return true;
    }

    public bool Register(string username, string password)
    {
        // TODO: Call API to register
        
        throw new NotImplementedException();
    }

    public void Logout()
    {
        // TODO: Call API to logout

        SaveToken(string.Empty);
    }

    private void SaveToken(string token)
    {
        _accountToken = token;
        AppSettings.UserToken = token;
    }
}