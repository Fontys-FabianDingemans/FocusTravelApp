using System.Diagnostics;
using FocusTravelApp.AppPermissions;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace FocusTravelApp.Managers;

public class BluetoothManager
{
    private Thread thread;
    private bool isRunning = false;
    
    private IReadOnlyList<IDevice> _pairedDevices = new List<IDevice>();

    public BluetoothManager()
    {
        this.thread = new Thread(this.StartBluetooth);
        this.thread.Start();
    }
    
    public void Run()
    {
        this.isRunning = true;
    }

    private async void StartBluetooth()
    {
        while (true)
        {
            if (!this.isRunning) continue;
        
            var status = await PermissionsHelper.CheckAndRequestBluetoothPermission();
            if (status != PermissionStatus.Granted)
            {
                Debug.WriteLine("Bluetooth permission not granted");
                this.isRunning = false;
                continue;
            }
            Debug.WriteLine("Bluetooth permission granted");
        
            Debug.WriteLine("Starting bluetooth");
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;

            this._pairedDevices = adapter.BondedDevices;
            foreach (var device in this._pairedDevices)
            {
                var deviceName = device.Name;
                var deviceId = device.Id;
            
                Debug.WriteLine($"Paired device: {deviceName}");
            }
        
            this.isRunning = false;
        }
    }
    
}