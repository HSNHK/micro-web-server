using System;
using MicroWebServer.WebServer;
using MicroWebServer.WebServer.Logging;
using System.Net;
namespace MicroWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLog consoleLog = new ConsoleLog();
            Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080, 10, consoleLog);
            if (server.start())
            {
                consoleLog.Informational("Started");
            } 
        }
    }
}
