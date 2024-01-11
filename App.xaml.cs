using System.Diagnostics;
using FocusTravelApp.Managers;

namespace FocusTravelApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        MainPage = new AppShell();
        
    }
}

