using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FocusTravelApp.ViewModel;
using JetBrains.Annotations;
using Debug = System.Diagnostics.Debug;


namespace FocusTravelApp;



public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel profileViewModel)
    {
        InitializeComponent();
        BindingContext = profileViewModel;
    }

    private void ClickedOnSettings(object sender, TappedEventArgs args)
    {
        Debug.WriteLine("Settings button tapped!");
        var SettingsViewModel = new SettingsViewModel();
        Navigation.PushAsync(new SettingsPage(SettingsViewModel));
    }
    
}