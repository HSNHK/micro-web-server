using MicroWebServer.WebServer;
using MicroWebServer.WebServer.IO;
using MicroWebServer.WebServer.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace MicroWebServer
{
    class Program
    {
        public static void Info(Requests requests, Response response)
        {
            Dictionary<string, string> myInfo = new Dictionary<string, string>()
            {
                {"name","HSNHK"},
                {"github","https://github.com/HSNHK" },
            };
            response.sendJson(myInfo, 200);

        }
        public static void Index(Requests requests, Response response)
        {
            response.header["time"] = DateTime.Now.ToString();
            response.send200Ok("Hello World!", response.extensions["txt"]);

        }
        public static void Programer(Requests requests, Response response)
        {
            if (requests.requestInfo["method"] == "POST")
            {
                response.send200Ok("i'm hassan mohammadi", response.extensions["html"]);
            }
            else
            {
                response.send200Ok("method not supported", response.extensions["html"]);
            }
        }
        static void Main(string[] args)
        {
            ConsoleLog consoleLog = new ConsoleLog();
            Dictionary<string, Action<Requests, Response>> urlPatterns = new Dictionary<string, Action<Requests, Response>>()
            {
                {"/",Index },
                {"/programer", Programer},
                {"/info", Info},
            };

            Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080, 10, urlPatterns, consoleLog);
            if (server.Start())
            {
                consoleLog.Informational("Started");
            }
            Console.ReadKey();
        }
    }
}
