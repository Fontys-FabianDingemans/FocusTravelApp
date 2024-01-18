using FocusTravelApp.Managers;
using Microsoft.Maui.Platform;

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

        Email = "";
        Password = "";
        
        EmailEntry.Text = Email;
        PasswordEntry.Text = Password;
    }


    private void SubmitForm(object? sender, EventArgs e)
    {
        LoginButton.IsEnabled = false;
        if(Platform.CurrentActivity?.CurrentFocus != null)
        {
            Platform.CurrentActivity?.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
        }
        
        if (Email.Length == 0 || Password.Length == 0)
        {
            DisplayAlert("Login Failed", "Email or password is empty", "OK");
            PasswordEntry.Text = "";
            LoginButton.IsEnabled = true;
            return;
        }

        _authManager.LoginAsync(Email, Password, (isSuccess, error) =>
        {
            if (isSuccess)
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Login Failed", error, "OK");
                PasswordEntry.Text = "";
                LoginButton.IsEnabled = true;
            }
        });
    }

    private void RegisterButtonClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new RegisterPage());
    }

    private void FocusPasswordEntry(object? sender, EventArgs e)
    {
        PasswordEntry.Focus();
    }
}