namespace FocusTravelApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
    }
    private void ProfileButtonClicked(object sender, EventArgs e)
    {
        Navigation.PopToRootAsync();
    }
}
