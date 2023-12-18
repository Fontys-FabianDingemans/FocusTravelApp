using FocusTravelApp.Enums;

namespace FocusTravelApp.Managers;

public class DriveTimeManager
{
    private DriveTimeStatus _status = DriveTimeStatus.Stopped;
    private int _currentDriveTime = 0;
    private readonly Action<string> _setDriveTimeTextCallback;
        
    public DriveTimeManager(Action<string> setDriveTimeTextCallback)
    {
        this._setDriveTimeTextCallback = setDriveTimeTextCallback;
        Thread driveTimeThread = new (() => {
            while(true){
                if(this._status != DriveTimeStatus.Running) continue;
                
                this._currentDriveTime++;
                this._setDriveTimeTextCallback(FormatSecsToHms(_currentDriveTime));
                
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
        this._setDriveTimeTextCallback(FormatSecsToHms(_currentDriveTime));
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