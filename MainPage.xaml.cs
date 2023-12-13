using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace FocusTravelApp;

public partial class MainPage : ContentPage
{
    private readonly Timer _driveTimer;
    private int _currentDriveTime = 0;
    
    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        
        _driveTimer = new Timer();
        _driveTimer.Interval = 1000;
        _driveTimer.Elapsed += DriveTimerElapsed;
    }

    private void StartStopButtonTapped(object sender, TappedEventArgs args)
    {
        Debug.WriteLine("StartStopButtonTapped");
        
        if(_driveTimer.Enabled){
            _driveTimer.Stop();
            StartStopButtonText.Text = "Start";
            StartStopButton.BackgroundColor = Color.FromRgb(0, 176, 92);
        }else{
            _currentDriveTime = 0;
            _driveTimer.Start();
            StartStopButtonText.Text = "Stop";
            StartStopButton.BackgroundColor = Color.FromRgb(255, 0, 0);
        }
    }
    
    private void DriveTimerElapsed(object sender, ElapsedEventArgs args)
    {
        _currentDriveTime++;
        
        var formattedText = FormatSecsToHMS(_currentDriveTime);
        DriveTimeText.Text = formattedText;
            
        Debug.WriteLine($"DriveTimerElapsed: {_currentDriveTime}");
        Debug.WriteLine($"DriveTimerElapsed: {formattedText}");
        
    }
    
    private string FormatSecsToHMS(int seconds){
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }

}