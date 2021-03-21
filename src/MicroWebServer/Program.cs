using System;
using MicroWebServer.WebServer;
using MicroWebServer.WebServer.Logging;
using MicroWebServer.WebServer.IO;
using System.Net;
using System.Collections.Generic;

namespace MicroWebServer
{
    class Program
    {
        public static void Hello(Requests requests,Response response)
        {
            response.header["time"] =DateTime.Now.ToString();
            response.send200Ok("Hello World!", response.extensions["htm"]);
        }
        public static void Programer(Requests requests, Response response)
        {
            if (requests.requestInfo["method"]=="POST")
            {
                response.send200Ok("i'm hassan mohammadi", response.extensions["htm"]);
            }
            else
            {
                response.send200Ok("method not supported", response.extensions["htm"]);
            }
        }
        static void Main(string[] args)
        {
            ConsoleLog consoleLog = new ConsoleLog();
            Dictionary<string, Action<Requests,Response>> urlPatterns = new Dictionary<string, Action<Requests, Response>>()
            {
                {"/hello",Hello },
                {"/programer", Programer},
            };

            Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080, 10,urlPatterns, consoleLog);
            if (server.start())
            {
                consoleLog.Informational("Started");
            }
            Console.ReadKey();
        }
    }
}
