using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using FocusTravelApp.Views;

namespace FocusTravelApp;

using FocusTravelApp.Managers;

public partial class MainPage : ContentPage
{
    private readonly DriveTimeManager _driveTimeManager;
    private readonly BluetoothManager _bluetoothManager;
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        
        _driveTimeManager = new DriveTimeManager(this.UpdateDriveTimeText, this.UpdateDriveDistanceText, this.ShowDrinkPopUpCallBack, this.ShowPauzePopUpCallBack);
        //_bluetoothManager = new BluetoothManager();
        
        //var authManager = new AuthManager();
        //if (!authManager.IsLoggedIn())
        //{
         //   Debug.WriteLine("User is not logged in! Redirecting to login page...");
        //}
        
        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
        {
            UpdateTimeLabel();
            return true;
        });
    }
    private void UpdateTimeLabel()
    {
        DateTime currentTime = DateTime.Now;
        
        if (currentTime.Hour >= 5 && currentTime.Hour <= 12)
        {
            LabelTijd.Text = "Goedemorgen, Rijd veilig!";
        }

        else if (currentTime.Hour >= 13 && currentTime.Hour <= 17)
        {
            LabelTijd.Text = "Goedemiddag, Rijd voorzichtig!";
        }
        
        else if (currentTime.Hour >= 17 && currentTime.Hour <= 24)
        {
            LabelTijd.Text = "Goedenavond, Fijne reis!!";
        }
        
        else if (currentTime.Hour >= 1 && currentTime.Hour <= 5)
        {
            LabelTijd.Text = "Goedenacht, Veilige reis!";
        }
        
        else
        {
            LabelTijd.Text = $"Huidige tijd: {currentTime.ToString("HH:mm:ss")}";
        }
    }

    private void StartStopButtonTapped(object sender, TappedEventArgs args)
    {
        if (this._driveTimeManager.IsRunning())
        {
            this._driveTimeManager.Pause();
            this.StartStopButtonText.Text = "Reset";
            this.StartStopButton.BackgroundColor = Color.FromRgb(255, 153, 0);
        }
        else if (this._driveTimeManager.IsPaused())
        {
            this._driveTimeManager.Stop();
            this.StartStopButtonText.Text = "Start";
            this.StartStopButton.BackgroundColor = Color.FromRgb(0, 176, 92);
        }
        else
        {
            this._driveTimeManager.Start();
            this.StartStopButtonText.Text = "Stop";
            this.StartStopButton.BackgroundColor = Color.FromRgb(255, 0, 0);
        }
    }

    private void UpdateDriveTimeText(string text)
    {
        Application.Current?.Dispatcher.Dispatch(() => { DriveTimeText.Text = text; });
    }

    private void UpdateDriveDistanceText(string text)
    {
        Application.Current?.Dispatcher.Dispatch(() => { DriveDistanceText.Text = text; });
    }

    private void PlayButton1Tapped(object sender, TappedEventArgs args)
    {
        var PlayVideos = new PlayVideos(1);
        Debug.WriteLine("Play button 1 tapped!");
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(PlayVideos); });
        PlayVideos.UpdateVideoPopUp();
    }

    private void PlayButton2Tapped(object sender, TappedEventArgs args)
    {
        var PlayVideos = new PlayVideos(2);
        Debug.WriteLine("Play button 2 Tapped!");
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(PlayVideos); });
        PlayVideos.UpdateVideoPopUp();
    }

    private void PlayButton3Tapped(object sender, TappedEventArgs args)
    {
        var PlayVideos = new PlayVideos(3);
        Debug.WriteLine("Play button 3 tapped!");
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(PlayVideos); });
        PlayVideos.UpdateVideoPopUp();
    }

    private void PlayButton4Tapped(object sender, TappedEventArgs args)
    {
        var PlayVideos = new PlayVideos(4);
        Debug.WriteLine("Play button 4 tapped!");
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(PlayVideos); });
        PlayVideos.UpdateVideoPopUp();
    }

    private void PlayButton5Tapped(object sender, TappedEventArgs args)
    {
        var PlayVideos = new PlayVideos(5);
        Debug.WriteLine("Play button 5 tapped!");
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(PlayVideos); });
        PlayVideos.UpdateVideoPopUp();
    }
    
    private void ShowDrinkPopUpCallBack(string text)
    {
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(new DrinkReminderPopUp()); });
    }

    private void ShowPauzePopUpCallBack(string text)
    {
        Application.Current?.Dispatcher.Dispatch(() => { this.ShowPopupAsync(new PauzeReminderPopUp()); });
    }
}