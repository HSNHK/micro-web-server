using MicroWebServer.WebServer;
using MicroWebServer.WebServer.IO;
using MicroWebServer.WebServer.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace MicroWebServer
{
    class Program
    {
        public static void Info(Requests requests, Response response)
        {
            Dictionary<string, string> myInfo = new Dictionary<string, string>()
            {
                {"name",requests.getArg("name","null")},
                {"age",requests.getArg("age","null") },
                {"github","https://github.com/HSNHK" },
            };
            response.sendJson(myInfo, 200);

        }
        public static void Index(Requests requests, Response response)
        {
            response.header["time"] = DateTime.Now.ToString();
            response.cookie["name"] = "HSNhk";
            response.setSecurityHeader();
            Console.WriteLine(requests.header["time"]);
            Console.WriteLine(requests.cookie["name"]);
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
                {@"^\/$",Index },
                {@"^\/programer$", Programer},
                {@"^\/info\?name\=[a-z]+\&age=\d+$", Info},
            };

            Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080, 10, urlPatterns, consoleLog);
            if (server.Start())
            {
                consoleLog.Informational("Started");
            }
            Thread.CurrentThread.Join();
        }
    }
}
