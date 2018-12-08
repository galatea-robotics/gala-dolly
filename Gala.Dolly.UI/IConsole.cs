using System;
using System.Collections.Generic;
using System.Text;

namespace Gala.Dolly.UI
{
    internal interface IConsole
    {
        void SendResponse(string response);
        bool IsSilent { get; set; }
    }
}
