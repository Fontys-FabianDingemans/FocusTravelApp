using FocusTravelApp.Managers;

namespace FocusTravelApp;

public partial class LoginPage : ContentPage
{
    private AuthManager _authManager;
    
    public string Email { get; set; }
    public string Password { get; set; }
    
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = this;
        
        _authManager = new AuthManager();

        Email = "mail@example.com";
        Password = "123456";
    }


    private void LoginButtonClicked(object? sender, EventArgs e)
    {
        if (Email.Length == 0 || Password.Length == 0)
        {
            DisplayAlert("Login Failed", "Email or password is empty", "OK");
            return;
        }
        
        if (_authManager.Login(Email, Password))
        {
            Navigation.PushAsync(new MainPage());
        }
        else
        {
            DisplayAlert("Login Failed", "Email or password is incorrect", "OK");
        }
    }

    private void RegisterButtonClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }
}