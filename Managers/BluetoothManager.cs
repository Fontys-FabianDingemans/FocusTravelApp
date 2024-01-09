using System.Diagnostics;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;

namespace FocusTravelApp.Managers;

public class BluetoothManager
{
    
    public void FindDeviceAndConnect(Action<string, Color, bool> setStatusTextCallback)
    {
        Thread t = new Thread(async () =>
        {
            String DeviceName = "FocusTravel";
            
            setStatusTextCallback("Selecteer apparaat...", Color.FromRgb(255, 114, 0), true);
            Debug.WriteLine("Opening BT picker...");
    
            BluetoothClient client = new BluetoothClient();
            BluetoothDevicePicker picker = new BluetoothDevicePicker();
            BluetoothDeviceInfo device = await picker.PickSingleDeviceAsync();
            
            Debug.WriteLine($"Selected device: {device.DeviceName}");
            
            client.Connect(device.DeviceAddress, BluetoothService.SerialPort);

            if (!device.Connected)
            {
                setStatusTextCallback("Fout bij verbinden", Color.FromRgb(255, 0, 0), true);
                return;
            }
            setStatusTextCallback("Verbonden", Color.FromRgb(0, 255, 0), true);
            
            // Send and receive data
            var stream = client.GetStream();
            
            // Read data
            StreamReader sr = new StreamReader(stream, System.Text.Encoding.ASCII);
            string line = sr.ReadLine();
            Debug.WriteLine(line);
            sr.Close();
            
            StreamWriter sw = new StreamWriter(stream, System.Text.Encoding.ASCII);
            sw.WriteLine("Hello world!\r\n\r\n");
            sw.Close();
            
        });
        t.Start();
    }
    
}