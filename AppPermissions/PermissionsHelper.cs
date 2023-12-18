namespace FocusTravelApp.AppPermissions;

public class PermissionsHelper
{

    public static async Task<PermissionStatus> CheckAndRequestBluetoothPermission()
    {
        return await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            var status = PermissionStatus.Unknown;

            status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
            if (status == PermissionStatus.Granted)
                return status;

            if (Permissions.ShouldShowRationale<Permissions.Bluetooth>())
            {
                await Application.Current?.MainPage.DisplayAlert("Bluetooth Permission", "Allow Focus to use Bluetooth", "OK");
            }

            status = await Permissions.RequestAsync<Permissions.Bluetooth>();

            if (status != PermissionStatus.Granted)
            {
                await Application.Current?.MainPage.DisplayAlert("Permission required", "Bluetooth permission is required", "OK");
            }

            return status;
        });
    }
    
}