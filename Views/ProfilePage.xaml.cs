using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusTravelApp.Managers;
using JetBrains.Annotations;
using Debug = System.Diagnostics.Debug;

namespace FocusTravelApp;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();
        BindingContext = this;
        
        LoadUserProfile();
    }

    private void ClickedOnSettings(object sender, TappedEventArgs args)
    {
        Navigation.PushAsync(new SettingsPage());
    }

    private void ClickedOnSignout(object? sender, TappedEventArgs e)
    {
        var authManager = new AuthManager();
        authManager.LogoutAsync((isSuccess, error) =>
        {
            if (!isSuccess)
            {
                DisplayAlert("Login Failed", error, "OK");
                return;
            }
            Navigation.PushAsync(new LoginPage());
        });
    }

    private void EditProfileButtonPressed(object? sender, TappedEventArgs e)
    {
        
    }

    private void LoadUserProfile()
    {
        var authManager = new AuthManager();
        authManager.GetUserAsync((isSuccess, error, userInstance) =>
        {
            if (!isSuccess || userInstance == null)
            {
                DisplayAlert("Error getting profile", error, "OK");
                return;
            }
            
            ProfileName.Text = userInstance.GetFullName();
            ProfileEmail.Text = userInstance.Email;
            if (userInstance.ProfilePictureUrl != null)
            {
                ProfileImage.Source = userInstance.ProfilePictureUrl;
            }
        });
    }

}