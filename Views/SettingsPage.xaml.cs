using System.Diagnostics;
using FocusTravelApp.Managers;
using FocusTravelApp.ViewModel;


namespace FocusTravelApp;

public partial class SettingsPage : ContentPage
{
    private BluetoothManager _bluetoothManager;
    
    public SettingsPage(SettingsViewModel settingsViewModel)
    {
        InitializeComponent();
        
        _bluetoothManager = new BluetoothManager();

        BreakReminderEntry.Text = AppSettings.BreakReminderInterval.ToString();
        DrinkReminderEntry.Text = AppSettings.DrinkReminderInterval.ToString();
    }

    private void BreakReminder_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        AppSettings.BreakReminderInterval = e.NewTextValue;
    }

    private void DrinkReminder_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        AppSettings.DrinkReminderInterval = e.NewTextValue;
    }

    private void SearchDeviceButton_OnClicked(object? sender, EventArgs e)
    {
        Debug.WriteLine("SearchDeviceButton_OnClicked");
        
        _bluetoothManager.FindDeviceAndConnect(this.UpdateBluetoothStatusText);
    }

    private void UpdateBluetoothStatusText(string text, Color? color)
    {
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            BluetoothStatusLabel.Text = text;
            BluetoothStatusLabel.TextColor = color ?? Color.FromRgb(255, 255, 255);
        });
    }
    
    private void ClickedOnGoBackToProfile(object sender, TappedEventArgs args)
    {
        Debug.WriteLine("Back to profile button tapped!");
        Navigation.PopAsync();
    }

}