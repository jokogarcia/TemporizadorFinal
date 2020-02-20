using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TemporizadorFinal.Services
{
    public interface IHC05Driver
    {
        Task Initialize(string DeviceName);
        Task PairToDevice(string DeviceName, string PIN);
        StreamReader Rx { get;  }
        StreamWriter Tx { get;  }
    }
}
