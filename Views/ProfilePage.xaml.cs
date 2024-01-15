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
    }

    private void ClickedOnSettings(object sender, TappedEventArgs args)
    {
        Debug.WriteLine("Settings button tapped!");
        Navigation.PushAsync(new SettingsPage());
    }

    private void ClickedOnSignout(object? sender, TappedEventArgs e)
    {
        var authManager = new AuthManager();
        authManager.Logout();
    }
}