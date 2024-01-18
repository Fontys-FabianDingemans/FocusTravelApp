using System.Diagnostics;
using CommunityToolkit.Maui.Core;
using FocusTravelApp.Enums;

namespace FocusTravelApp.Managers;

public class DriveTimeManager
{
    private DriveTimeStatus _status = DriveTimeStatus.Stopped;
    private int _currentDriveTime = 0;
    private int _currentDriveDistanceInMeters = 0;
    
    private readonly Action<string> _setDriveTimeTextCallback;
    private readonly Action<string> _setDriveDistanceTextCallback;
    private readonly Action<string> _showDrinkPopup;
    private readonly Action<string> _showPauzePopup;
        
    public DriveTimeManager(Action<string> setDriveTimeTextCallback, Action<string> setDriveDistanceTextCallback, Action<string> showDrinkPopup, Action<String> showPauzePopup)
    {
        this._setDriveTimeTextCallback = setDriveTimeTextCallback;
        this._setDriveDistanceTextCallback = setDriveDistanceTextCallback;
        this._showDrinkPopup = showDrinkPopup;
        this._showPauzePopup = showPauzePopup;
        
        
        Thread driveTimeThread = new (() => {
            while(true){
                if(this._status != DriveTimeStatus.Running) continue;
                
                this._currentDriveTime++;
                this._setDriveTimeTextCallback(FormatSecsToHms(_currentDriveTime));
                
                if(new Random().Next(0, 100) < 30)//<-- Chance percentage
                {
                    var distanceToAdd = new Random().Next(100, 200);
                    this._currentDriveDistanceInMeters += distanceToAdd;
                    
                    var distanceInKm = ((decimal) this._currentDriveDistanceInMeters) / 1000;
                    
                    this._setDriveDistanceTextCallback(string.Format("{0:0.0}", distanceInKm) + " KM");
                }

                // Break reminder in minutes
                var breakReminderInterval = Int32.Parse(AppSettings.BreakReminderInterval);
                if (_currentDriveTime % (breakReminderInterval * 60) == 0)
                {
                    this._showPauzePopup("");
                }
                
                // Drink reminder in minutes
                var drinkReminderInterval = Int32.Parse(AppSettings.DrinkReminderInterval);
                if (_currentDriveTime % (drinkReminderInterval * 60) == 0)
                {
                    this._showDrinkPopup("");
                }
                
                Thread.Sleep(1000);
            }
        });
        driveTimeThread.Start();
    }
    
    /**
     * Start the timer
     */
    public void Start()
    {
        this._status = DriveTimeStatus.Running;
    }

    /**
     * Pause the timer without resetting
     */
    public void Pause()
    {
        this._status = DriveTimeStatus.Paused;
    }
    
    /**
     * Stop the timer and reset
     */
    public void Stop()
    {
        this._status = DriveTimeStatus.Stopped;
        this._currentDriveTime = 0;
        this._currentDriveDistanceInMeters = 0;
        
        this._setDriveTimeTextCallback(FormatSecsToHms(_currentDriveTime));
        this._setDriveDistanceTextCallback("0 KM");
    }
    
    /**
     * Returns true or false if the timer is running
     */
    public bool IsRunning()
    {
        return this._status == DriveTimeStatus.Running;
    }
    
    /**
     * Returns true or false if the timer is paused
     */
    public bool IsPaused()
    {
        return this._status == DriveTimeStatus.Paused;
    }
    
    public int GetDriveTime(){
        return this._currentDriveTime;
    }
    
    public string GetFormattedDriveTime(){
        return FormatSecsToHms(this._currentDriveTime);
    }
    
    private string FormatSecsToHms(int seconds){
        var timeSpan = TimeSpan.FromSeconds(seconds);
        return $"{(int)timeSpan.TotalHours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
    
}