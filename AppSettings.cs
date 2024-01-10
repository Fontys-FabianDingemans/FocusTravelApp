namespace FocusTravelApp;

public static class AppSettings
{
    
    public static string BreakReminderInterval
    {
        get => Preferences.Default.Get("BREAK_REMINDER_INTERVAL", "30");
        set => Preferences.Default.Set("BREAK_REMINDER_INTERVAL", value);
    }
    
    public static string DrinkReminderInterval
    {
        get => Preferences.Default.Get("DRINK_REMINDER_INTERVAL", "45");
        set => Preferences.Default.Set("DRINK_REMINDER_INTERVAL", value);
    }
    
    public static string UserToken
    {
        get => Preferences.Default.Get("USER_TOKEN", "");
        set => Preferences.Default.Set("USER_TOKEN", value);
    }
    
}