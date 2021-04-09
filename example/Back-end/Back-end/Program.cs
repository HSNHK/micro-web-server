using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicroWebServer.WebServer;
using MicroWebServer.WebServer.IO;
using MicroWebServer.WebServer.Logging;
using Back_end.Data;
using Microsoft.EntityFrameworkCore;
using Back_end.Business;
using System.Linq;
namespace Back_end
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }
        private static Business.Information information;
        public static void LoadConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables().Build();
        }

        static void Main(string[] args)
        {
            LoadConfiguration();
            Data.PeoplesContext peoplesContext = new PeoplesContext();
            information = new Business.Information(peoplesContext);
            //ConsoleLog consoleLog = new ConsoleLog();
            //Dictionary<string, Action<Requests, Response>> urlPatterns = new Dictionary<string, Action<Requests, Response>>()
            //{
            //    //{@"^\/$",Index },
            //    //{@"^\/programer$", Programer},
            //    //{@"^\/info\?name\=[a-z]+\&age=\d+$", Info},
            //};

            //Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080, 10, urlPatterns, consoleLog);
            //if (server.Start())
            //{
            //    consoleLog.Informational("Started");
            //}
            //Thread.CurrentThread.Join();
        }
        
    }
}
