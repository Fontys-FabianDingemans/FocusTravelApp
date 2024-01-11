using FocusTravelApp.Views;

namespace FocusTravelApp;

public partial class AppShell : Shell
{
    
    
    protected override void OnNavigating(ShellNavigatingEventArgs args)
    {
        base.OnNavigating(args);

        if (args.Source == ShellNavigationSource.ShellSectionChanged)
        {
            var navigation = Shell.Current.Navigation;
            var pages = navigation.NavigationStack;
            for (var i = pages.Count - 1; i >= 1; i--)
            {
                navigation.RemovePage(pages[i]);
            }
        }
    }
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
    }
    
   
    private void ProfileButtonClicked(object sender ,EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
    
    
}

    
