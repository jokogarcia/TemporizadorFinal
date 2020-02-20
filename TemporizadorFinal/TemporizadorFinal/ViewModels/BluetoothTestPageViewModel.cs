using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using TemporizadorFinal.Services;
using Android.Bluetooth;
using System.Threading.Tasks;
using System.Text;

namespace TemporizadorFinal.ViewModels
{
    public class BluetoothTestPageViewModel : BindableBase
    {

        #region PRIVATE_FIELDS
        private DelegateCommand _getStatus;
        private DelegateCommand _listDevices;
        BluetoothSocket _socket;
        private bool _listDevicesCanExecute;
        bool ListDevicesCanExecute { get => _listDevicesCanExecute;
            set {
                _listDevicesCanExecute = value;
                ListDevices.RaiseCanExecuteChanged();
            
            }
        }
        private string _currentStatus;
        string log;
        List<string> uuidList;

        private IPermissionsService _permissionsService;
        #endregion

        #region PUBLIC_DECLARATIONS
        public DelegateCommand GetStatus { get => _getStatus ?? (_getStatus = new DelegateCommand(GetStatus_Execute)); }
        public string CurrentStatus { get => _currentStatus; set => SetProperty(ref _currentStatus, value); }
        private void GetStatus_Execute()
        {
            

        }

        public DelegateCommand ListDevices { get => _listDevices ?? (_listDevices = new DelegateCommand(ListDevices_Execute,()=>_listDevicesCanExecute)); }

        private async void ListDevices_Execute()
        {
            var buffer = Encoding.ASCII.GetBytes("I am the Walrus!");
            if (_listDevicesCanExecute)
            {
                await _socket.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            }
           
        }

        

      

        #endregion

        public BluetoothTestPageViewModel(IPermissionsService permissionsService)
        {
            _permissionsService = permissionsService;
            log = "";
            uuidList = new List<string>();
            ListDevicesCanExecute = false;
            Connect();
        }
        async Task Connect()
        {
           
            if (!_permissionsService.CheckLocationPermission())
            {
                if (!await _permissionsService.RequestLocationPermission())
                {
                    throw new Exception("Debe proveer permisos");
                }
            }
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
            ListDevicesCanExecute = true;
        }
    }
}
