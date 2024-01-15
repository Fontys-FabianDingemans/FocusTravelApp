using System.Diagnostics;
using System.Net.Sockets;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace FocusTravelApp.Managers;

public class BluetoothManager
{
    
    private BluetoothClient _client;
    private NetworkStream? _stream;
    private Action<string, Color?, bool, bool>? _setStatusTextCallback;

    public BluetoothManager()
    {
        _client = new BluetoothClient();
        
        Init();
    }
    
    public BluetoothManager(Action<string, Color?, bool, bool> setStatusTextCallback)
    {
        _client = new BluetoothClient();
        _setStatusTextCallback = setStatusTextCallback;
        
        Init();
    }

    public void SetStatusTextCallback(Action<string, Color?, bool, bool> setStatusTextCallback)
    {
        _setStatusTextCallback = setStatusTextCallback;
    }

    private void UpdateStatusText(string text, Color? color, bool enableSearchButton, bool enableSendButton)
    {
        _setStatusTextCallback?.Invoke(text, color, enableSearchButton, enableSendButton);
    }

    private void Init()
    {
        if (_client.Connected)
        {
            UpdateStatusText($"Verbonden", Color.FromRgb(0, 255, 0), false, true);
        }
        else
        {
            UpdateStatusText("Niet verbonden", Color.FromRgb(255, 255, 255), true, false);
        }
    }
    
    public void FindDeviceAndConnect()
    {
        var t = new Thread(async () =>
        {
            UpdateStatusText("Apparaat zoeken...", Color.FromRgb(255, 114, 0), false, false);

            BluetoothClient client = new BluetoothClient();
            BluetoothDeviceInfo device = null;
            foreach(var dev in client.PairedDevices)
            {
                Debug.WriteLine($"Found: {dev.DeviceName}");
                if(dev.DeviceName == "FocusTravel")
                {
                    device = dev;
                    break;
                }
            }
            
            Debug.WriteLine($"Loop complete");

            if (device == null)
            {
                Debug.WriteLine($"Device null");
                UpdateStatusText("Apparaat niet gevonden", Color.FromRgb(255, 0, 0), true, false);
                return;
            }
            
            Debug.WriteLine($"Device not null");
            
            if (!device.Authenticated)
            {
                BluetoothSecurity.PairRequest(device.DeviceAddress, "0000");
                Debug.WriteLine($"Not authenticated");
            }
            
            Debug.WriteLine($"Connecting to device");
            
            UpdateStatusText($"Verbinden met {device.DeviceName}...", Color.FromRgb(255, 114, 0), false, false);
            
            await _client.ConnectAsync(device.DeviceAddress, BluetoothService.SerialPort);
            Debug.WriteLine($"Checking connected");

            if (!device.Connected)
            {
                Debug.WriteLine($"Error at connecting");
                UpdateStatusText("Fout bij verbinden", Color.FromRgb(255, 0, 0), true, false);
                return;
            }
            Debug.WriteLine($"Connected to device");
            UpdateStatusText($"Verbonden met {device.DeviceName}", Color.FromRgb(0, 255, 0), false, true);
            
            // Send and receive data
            Debug.WriteLine($"Get stream");
            _stream = _client.GetStream();
            Debug.WriteLine($"Got stream");
            
            
            // Read data
            // var sr = new StreamReader(stream, System.Text.Encoding.ASCII);
            //
            // while (device.Connected)
            // {
            //     var line = await sr.ReadLineAsync();
            //     Debug.WriteLine(line is { Length: > 0 } ? line : "No data received");
            //     Thread.Sleep(100);
            // }
            //
            // sr.Close();
            
        });
        t.Start();
    }

    public void SendMessage()
    {
        //TODO: App craching when clicking button for the second time
        var t = new Thread(async () =>
        {
            if (_stream == null)
            {
                Debug.WriteLine("Stream is null");
                return;
            }
            Debug.WriteLine($"Stream valid");
            
            // Write data
            var streamWriter = new StreamWriter(_stream, System.Text.Encoding.ASCII);
            Debug.WriteLine($"Send message");
            await streamWriter.WriteLineAsync("W");
            streamWriter.Close();
            Debug.WriteLine($"Closed stream writer connection");
        });
        t.Start();
    }
    
}