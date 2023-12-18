using System.Diagnostics;

namespace FocusTravelApp;

using FocusTravelApp.Managers;

public partial class MainPage : ContentPage
{
    private readonly DriveTimeManager _driveTimeManager;
    private readonly BluetoothManager _bluetoothManager;
    
    public MainPage()
    {
        InitializeComponent();
        this._driveTimeManager = new DriveTimeManager(this.UpdateDriveTimeText, this.UpdateDriveDistanceText);
        this._bluetoothManager = new BluetoothManager();
    }

    private void StartStopButtonTapped(object sender, TappedEventArgs args)
    {
        if(this._driveTimeManager.IsRunning()){
            this._driveTimeManager.Pause();
            this.StartStopButtonText.Text = "Reset";
            this.StartStopButton.BackgroundColor = Color.FromRgb(255, 153, 0);
        }else if(this._driveTimeManager.IsPaused()){
            this._driveTimeManager.Stop();
            this.StartStopButtonText.Text = "Start";
            this.StartStopButton.BackgroundColor = Color.FromRgb(0, 176, 92);
        }else{
            this._driveTimeManager.Start();
            this.StartStopButtonText.Text = "Stop";
            this.StartStopButton.BackgroundColor = Color.FromRgb(255, 0, 0);
        }
    }
    
    private void UpdateDriveTimeText(string text)
    {
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            DriveTimeText.Text = text;
        });
    }
    
    private void UpdateDriveDistanceText(string text)
    {
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            DriveDistanceText.Text = text;
        });
    }
    
    public void TempButtonClicked(object sender, EventArgs args)
    {
        Debug.WriteLine("Temp button clicked");
        this._bluetoothManager.Run();
    }
    
}