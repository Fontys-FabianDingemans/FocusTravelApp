using System.Diagnostics;
using FocusTravelApp.Enums;

namespace FocusTravelApp.Managers;

public class DriveTimeManager
{
    private DriveTimeStatus _status = DriveTimeStatus.Stopped;
    private int _currentDriveTime = 0;
    private int _currentDriveDistanceInMeters = 0;
    
    private readonly Action<string> _setDriveTimeTextCallback;
    private readonly Action<string> _setDriveDistanceTextCallback;
        
    public DriveTimeManager(Action<string> setDriveTimeTextCallback, Action<string> setDriveDistanceTextCallback)
    {
        this._setDriveTimeTextCallback = setDriveTimeTextCallback;
        this._setDriveDistanceTextCallback = setDriveDistanceTextCallback;
        
        
        Thread driveTimeThread = new (() => {
            while(true){
                if(this._status != DriveTimeStatus.Running) continue;
                
                this._currentDriveTime++;
                this._setDriveTimeTextCallback(FormatSecsToHms(_currentDriveTime));
                
                if(new Random().Next(0, 100) < 50)//<-- Chance percentage
                {
                    var distanceToAdd = new Random().Next(100, 300);
                    this._currentDriveDistanceInMeters += distanceToAdd;
                    
                    var distanceInKm = ((decimal) this._currentDriveDistanceInMeters) / 1000;
                    
                    Debug.WriteLine(this._currentDriveDistanceInMeters);
                    Debug.WriteLine(distanceInKm);
                    
                    this._setDriveDistanceTextCallback(string.Format("{0:0.0}", distanceInKm) + " KM");
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