using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using TemporizadorFinal.Services;
using Xamarin.Forms;

namespace TemporizadorFinal.Droid.DependencyServices
{
    class PermissionsService : IPermissionsService
    {
        bool _isReady;
        bool _result;
        public bool CheckLocationPermission()
        {
            return ContextCompat.CheckSelfPermission(MainActivity.Context, Manifest.Permission.AccessCoarseLocation) == (int)Permission.Granted ||
                ContextCompat.CheckSelfPermission(MainActivity.Context, Manifest.Permission.AccessFineLocation) == (int)Permission.Granted;
        }

        public async Task<bool> RequestLocationPermission()
        {
            _isReady = false;
            ((Activity)MainActivity.Context).RequestPermissions(new String[] { Manifest.Permission.AccessCoarseLocation },2719);
            MessagingCenter.Subscribe<MainActivity, bool>(this, "PERMISSION_RESULT", ProcessResult);
            while (!_isReady)
            {
                await Task.Delay(250);
            }
            return _result;
        }

        private void ProcessResult(MainActivity arg1, bool arg2)
        {
            _result = arg2;
            _isReady = true;
        }
    }
}