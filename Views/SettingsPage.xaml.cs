using System.Diagnostics;
using FocusTravelApp.Managers;


namespace FocusTravelApp;

public partial class SettingsPage : ContentPage
{
    private BluetoothManager _bluetoothManager;
    
    public SettingsPage()
    {
        InitializeComponent();
        BindingContext = this;
        
       // _bluetoothManager = new BluetoothManager(this.UpdateBluetoothStatusText);

        BreakReminderEntry.Text = AppSettings.BreakReminderInterval;
        DrinkReminderEntry.Text = AppSettings.DrinkReminderInterval;
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
        
        _bluetoothManager.FindDeviceAndConnect();
    }

    private void UpdateBluetoothStatusText(string text, Color? color, bool enableSearchButton, bool enableSendButton)
    {
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            BluetoothStatusLabel.Text = text;
            BluetoothStatusLabel.TextColor = color ?? Color.FromRgb(255, 255, 255);
            SearchDeviceButton.IsEnabled = enableSearchButton;
            SendMessageButton.IsEnabled = enableSendButton;
        });
    }
    
    private void ClickedOnGoBackToProfile(object sender, TappedEventArgs args)
    {
        Navigation.PopAsync();
    }

    private void SendMessageButton_OnClicked(object? sender, EventArgs e)
    {
        _bluetoothManager.SendMessage();
    }
}