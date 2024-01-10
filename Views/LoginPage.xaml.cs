using FocusTravelApp.Managers;
using FocusTravelApp.Models;
using FocusTravelApp.ViewModel;

namespace FocusTravelApp;

public partial class LoginPage : ContentPage
{
    private AuthManager _authManager;
    
    public string Username { get; set; }
    public string Password { get; set; }
    
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
        
        _authManager = new AuthManager();

        Username = "";
        Password = "";
    }


    private void LoginButtonClicked(object? sender, EventArgs e)
    {
        if (Username.Length == 0 || Password.Length == 0)
        {
            DisplayAlert("Login Failed", "Username or password is empty", "OK");
            return;
        }
        
        if (_authManager.Login(Username, Password))
        {
            var mainViewModel = new MainViewModel();
            Navigation.PushAsync(new MainPage(mainViewModel));
        }
        else
        {
            DisplayAlert("Login Failed", "Username or password is incorrect", "OK");
        }
    }

    private void RegisterButtonClicked(object? sender, EventArgs e)
    {
        var registerViewModel = new RegisterViewModel();
        Navigation.PushAsync(new RegisterPage(registerViewModel));
    }
}