using System.Diagnostics;
using FocusTravelApp.AppPermissions;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace FocusTravelApp.Managers;

public class BluetoothManager
{
    private Thread thread;
    private bool isRunning = false;
    private bool bluetoothAllowed = false;
    
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
    
    public void Stop()
    {
        this.isRunning = false;
    }

    private async Task<bool> CheckPermission()
    {
        var status = await PermissionsHelper.CheckAndRequestBluetoothPermission();
        if (status != PermissionStatus.Granted)
        {
            Debug.WriteLine("Bluetooth permission not granted");
            this.isRunning = false;
            bluetoothAllowed = false;
            return bluetoothAllowed;
        }
        Debug.WriteLine("Bluetooth permission granted");
        bluetoothAllowed = true;
        return bluetoothAllowed;
    }

    private void StartBluetooth()
    {
        while (true)
        {
            if (!this.isRunning) continue;
            if (this.CheckPermission().Result) continue;
        
            Debug.WriteLine("Starting bluetooth");
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;

            this._pairedDevices = adapter.BondedDevices;
            foreach (var device in this._pairedDevices)
            {
                var deviceName = device.Name;
                var deviceId = device.Id;
            
                Debug.WriteLine($"Paired device: {deviceName}");
                adapter.ConnectToDeviceAsync(device);
            }
        
            this.isRunning = false;
        }
    }

    public void FindDeviceAndConnect(Action<string, Color> setStatusTextCallback)
    {
        Thread t = new Thread(() =>
        {
            if (this.CheckPermission().Result) return;
            bool deviceFound = false;
        
            var ble = CrossBluetoothLE.Current;
            var adapter = CrossBluetoothLE.Current.Adapter;
            
            setStatusTextCallback("Searching...", Color.FromRgb(255, 255, 255));
        
            this._pairedDevices = adapter.BondedDevices;
            foreach (var device in this._pairedDevices)
            {
                var deviceName = device.Name;
                var deviceId = device.Id;
                
                if(deviceName == null) return;
                
                if (deviceName == "FocusTravelTest2")
                {
                    // Connect to to device
                    adapter.ConnectToDeviceAsync(device);
                    deviceFound = true;
                    setStatusTextCallback("Device connected", Color.FromRgb(0, 255, 0));
                }
                
                Debug.WriteLine($"Paired device: {deviceName}");
            }

            if (!deviceFound)
            {
                setStatusTextCallback("Device not found", Color.FromRgb(255, 0, 0));
            }
        });
        t.Start();
    }
    
}