using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using MicroWebServer.WebServer.Logging;
using MicroWebServer.WebServer.IO;
namespace MicroWebServer.WebServer
{
    public class Server
    {
        private Encoding charEncoder = Encoding.UTF8;
        private Socket serverSocket;
        private IPAddress ipAddress;
        private Response Response;
        private int maxOfConnections { get; set; }
        private int timeout { get; set; }
        private ILog _log { get; set; }
        private int port { get; set; }
        public bool running = false;
 
        public Server(IPAddress ipAddress, int port, int maxOfConnections,ConsoleLog consoleLog)
        {
            this.ipAddress = ipAddress;
            this.port = port;

            this.maxOfConnections = maxOfConnections;
            this.timeout = 8;

            Response = new Response();
            _log = consoleLog;
        }
        public Server(IPAddress ipAddress, int port, int maxOfConnections, SysLog sysLog)
        {
            this.ipAddress = ipAddress;
            this.port = port;

            this.maxOfConnections = maxOfConnections;
            this.timeout = 8;

            Response = new Response();
            _log = sysLog;
        }
        public bool start()
        {
            if (running) return false;
            try
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ipAddress, port));
                serverSocket.Listen(maxOfConnections);
                serverSocket.ReceiveTimeout = timeout;
                serverSocket.SendTimeout = timeout;
                running = true;
            }
            catch
            {
                _log.Critical("Problem setting up the server");
                return false; 
            }

            Thread requestListenerT = new Thread(() =>
            {
                while (running)
                {
                    Socket clientSocket;
                    try
                    {
                        clientSocket = serverSocket.Accept();
                        Thread requestHandler = new Thread(() =>
                        {
                            clientSocket.ReceiveTimeout = timeout;
                            clientSocket.SendTimeout = timeout;
                            try { handleTheRequest(clientSocket); }
                            catch
                            {
                                try { clientSocket.Close(); } catch { }
                            }
                        });
                        requestHandler.Start();
                    }
                    catch { }
                }
            });
            requestListenerT.Start();

            return true;
        }
        public void stop()
        {
            if (running)
            {
                running = false;
                try 
                {
                    serverSocket.Close();
                }
                catch 
                {
                    _log.Error("Problem closing the socket");
                }
                serverSocket = null;
            }
        }
        private void handleTheRequest(Socket clientSocket)
        {
            byte[] buffer = new byte[10240];
            int receivedBCount = clientSocket.Receive(buffer);
            string strReceived = charEncoder.GetString(buffer, 0, receivedBCount);
            string httpMethod = strReceived.Substring(0, strReceived.IndexOf(" "));
            int start = strReceived.IndexOf(httpMethod) + httpMethod.Length + 1;
            int length = strReceived.LastIndexOf("HTTP") - start - 1;
            string requestedUrl = strReceived.Substring(start, length);
            _log.Informational($"{requestedUrl} {httpMethod} {length}");
            if (httpMethod.Equals("GET") || httpMethod.Equals("POST"))
            {
                if (requestedUrl=="/")
                {
                    Response.send200Ok(clientSocket, "hello world !!!", Response.extensions["txt"]);
                }
            }
            else
            {
                return;
            }
        }
    }
}
