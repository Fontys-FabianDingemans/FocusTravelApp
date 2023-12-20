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
    
}