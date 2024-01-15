namespace FocusTravelApp.Classes;

public class TruckHealthApp
{
    
    
    public string AppName { get; private set; } 
    public int Version { get; private set; } 
    public DateTime LastUpdate { get; private set; } 
    public bool IsConnectedToTruck { get; private set; }

    public TruckHealthApp(string appName, int version)
    {
        AppName = appName;
        Version = version;
        LastUpdate = DateTime.Now;
        IsConnectedToTruck = false;
    }

    public void ConnectToTruck()
    {
        if (!IsConnectedToTruck)
        {
            Console.WriteLine($"Aan het verbinden met de vrachtwagen via {AppName} v{Version}");
            IsConnectedToTruck = true;
        }
        else
        {
            Console.WriteLine("De app is al verbonden met de vrachtwagen!"); 
        }
            
    }

    public void GetTruckHealthInfo()
    {
        if (IsConnectedToTruck)
        {
            Console.WriteLine("Gegevens van de vrachtwagen ophalen...");
        }

        else
        {
            Console.WriteLine("Verbind eerst met de vrachtwagen.");
        }
            
    }
    
    public void UpdateApp(int newVersion)
    {
        if (newVersion > Version)
        {
            Version = newVersion;
            LastUpdate = DateTime.Now;
            Console.WriteLine($"App bijgewerkt naar v{Version}. Laatst bijgewerkt op {LastUpdate}");
        }
        else
        {
            Console.WriteLine("De app is al bijgewerkt naar de nieuwste versie.");
        }
    }
    
    public void SetAppName(string newAppName)
    {
        if (!string.IsNullOrWhiteSpace(newAppName))
        {
            AppName = newAppName;
            Console.WriteLine($"App-naam gewijzigd naar: {newAppName}");
        }
        else
        {
            Console.WriteLine("Dit is geen geldige app naam, probeer het opnieuw.");
        }
    }
    
}






