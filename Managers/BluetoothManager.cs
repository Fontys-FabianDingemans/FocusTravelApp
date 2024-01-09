using System.Diagnostics;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace FocusTravelApp.Managers;

public class BluetoothManager
{
    
    private BluetoothClient _client;
    private Action<string, Color?, bool?> _setStatusTextCallback;

    public BluetoothManager(Action<string, Color?, bool?> setStatusTextCallback)
    {
        _client = new BluetoothClient();
        _setStatusTextCallback = setStatusTextCallback;
        
        Init();
    }

    private void Init()
    {
        if (_client.Connected)
        {
            _setStatusTextCallback($"Verbonden", Color.FromRgb(0, 255, 0), false);
        }
        else
        {
            _setStatusTextCallback("Niet verbonden", Color.FromRgb(255, 255, 255), true);
        }
        
        
        
    }
    
    public void FindDeviceAndConnect()
    {
        var t = new Thread(async () =>
        {
            _setStatusTextCallback("Selecteer apparaat...", Color.FromRgb(255, 114, 0), false);
            
            var picker = new BluetoothDevicePicker();
            var device = await picker.PickSingleDeviceAsync();
            
            _setStatusTextCallback($"Verbinden met {device.DeviceName}...", Color.FromRgb(255, 114, 0), false);
            
            await _client.ConnectAsync(device.DeviceAddress, BluetoothService.SerialPort);

            if (!device.Connected)
            {
                _setStatusTextCallback("Fout bij verbinden", Color.FromRgb(255, 0, 0), true);
                return;
            }
            _setStatusTextCallback($"Verbonden met {device.DeviceName}", Color.FromRgb(0, 255, 0), false);
            
            // Send and receive data
            var stream = _client.GetStream();
            
            // Write data
            var sw = new StreamWriter(stream, System.Text.Encoding.ASCII);
            await sw.WriteLineAsync("Phone connected!\r\n\r\n");
            sw.Close();
            
            // Read data
            var sr = new StreamReader(stream, System.Text.Encoding.ASCII);
            
            while (device.Connected)
            {
                var line = await sr.ReadLineAsync();
                Debug.WriteLine(line is { Length: > 0 } ? line : "No data received");
                Thread.Sleep(100);
            }
            
            sr.Close();
            
        });
        t.Start();
    }
    
}