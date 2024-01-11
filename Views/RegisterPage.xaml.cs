using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusTravelApp.Managers;
using FocusTravelApp.ViewModel;
using JetBrains.Annotations;
using Debug = System.Diagnostics.Debug;

namespace FocusTravelApp;

public partial class RegisterPage : ContentPage
{
    private AuthManager _authManager;
    
    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    
    public RegisterPage(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        BindingContext = registerViewModel;
        
        _authManager = new AuthManager();
        
        Username = "";
        Password = "";
        PasswordConfirm = "";
    }

    private void RegisterButtonClicked(object? sender, EventArgs e)
    {
        if (Username.Length == 0 || Password.Length == 0 || PasswordConfirm.Length == 0)
        {
            DisplayAlert("Login Failed", "Username or password is empty", "OK");
            return;
        }
        
        if (Password != PasswordConfirm)
        {
            DisplayAlert("Login Failed", "Passwords do not match", "OK");
            return;
        }
        
        if (_authManager.Register(Username, Password))
        {
            var mainViewModel = new MainViewModel();
            Navigation.PushAsync(new MainPage(mainViewModel));
        }
        else
        {
            DisplayAlert("Login Failed", "Username or password is incorrect", "OK");
        }
    }

    private void LoginButtonClicked(object? sender, EventArgs e)
    {
        var loginViewModel = new LoginViewModel();
        Navigation.PushAsync(new LoginPage(loginViewModel));
    }
}