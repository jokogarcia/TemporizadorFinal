using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TemporizadorFinal.Services
{
    public interface IPermissionsService
    {
        bool CheckLocationPermission();
        Task<bool> RequestLocationPermission();
    }
}
