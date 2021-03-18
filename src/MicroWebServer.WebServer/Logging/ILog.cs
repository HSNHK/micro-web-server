using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroWebServer.WebServer.Logging
{
    interface ILog
    {
        Task Informational(string Message);
        Task Warning(string Message);
        Task Alert(string Message);
        Task Error(string Message);
        Task Critical(string Message);
        Task Debug(string Message);
    }
}
