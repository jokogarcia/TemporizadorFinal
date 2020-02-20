using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TemporizadorFinal.Services;

namespace TemporizadorFinal.Droid.DependencyServices
{
    public class HC05Driver : IHC05Driver
    {
        BluetoothSocket _socket;
        StreamReader streamReader;
        StreamWriter streamWriter;

        public StreamReader Rx { get => streamReader; }
        public StreamWriter Tx { get => streamWriter; }

        public async Task Initialize(string DeviceName)
        {
            BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
            if (adapter == null)
                throw new Exception("No Bluetooth adapter found.");

            if (!adapter.IsEnabled)
                throw new Exception("Bluetooth adapter is not enabled.");

            BluetoothDevice device = (from bd in adapter.BondedDevices
                                      where bd.Name == "HC-05"
                                      select bd).FirstOrDefault();

            if (device == null)
                throw new Exception("Named device not found.");
            _socket = device.CreateRfcommSocketToServiceRecord(Java.Util.UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));
            await _socket.ConnectAsync();
            streamReader = new StreamReader(_socket.InputStream);
            streamWriter = new StreamWriter(_socket.OutputStream);
        }

        public Task PairToDevice(string DeviceName, string PIN)
        {
            throw new NotImplementedException();
        }

     
    }
}