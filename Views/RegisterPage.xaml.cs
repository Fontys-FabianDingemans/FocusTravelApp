using FocusTravelApp.Managers;
using Microsoft.Maui.Platform;

namespace FocusTravelApp;

public partial class RegisterPage : ContentPage
{
    private AuthManager _authManager;
    
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    
    public RegisterPage()
    {
        InitializeComponent();
        BindingContext = this;
        
        _authManager = new AuthManager();
        
        Email = "";
        Password = "";
        PasswordConfirm = "";
    }

    private void SubmitForm(object? sender, EventArgs e)
    {
        RegisterButton.IsEnabled = false;
        if(Platform.CurrentActivity?.CurrentFocus != null)
        {
            Platform.CurrentActivity?.HideKeyboard(Platform.CurrentActivity.CurrentFocus);
        }
        
        if (Email.Length == 0 || Password.Length == 0 || PasswordConfirm.Length == 0)
        {
            DisplayAlert("Login Failed", "Username or password is empty", "OK");
            PasswordEntry.Text = "";
            PasswordConfirmEntry.Text = "";
            RegisterButton.IsEnabled = true;
            return;
        }
        
        if (Password != PasswordConfirm)
        {
            DisplayAlert("Login Failed", "Passwords do not match", "OK");
            PasswordEntry.Text = "";
            PasswordConfirmEntry.Text = "";
            RegisterButton.IsEnabled = true;
            return;
        }

        _authManager.RegisterAsync(Email, Password, PasswordConfirm, (isSuccess, error) =>
        {
            if (isSuccess)
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Register Failed", error, "OK");
                PasswordEntry.Text = "";
                PasswordConfirmEntry.Text = "";
                RegisterButton.IsEnabled = true;
            }
        });
    }

    private void LoginButtonClicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(new LoginPage());
    }

    private void FocusPasswordEntry(object? sender, EventArgs e)
    {
        PasswordEntry.Focus();
    }

    private void FocusPasswordConfirmEntry(object? sender, EventArgs e)
    {
        PasswordConfirmEntry.Focus();
    }
}