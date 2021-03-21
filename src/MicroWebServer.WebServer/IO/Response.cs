using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace MicroWebServer.WebServer.IO
{
    public class Response
    {
        private Encoding charEncoder = Encoding.UTF8;
        private Socket clientSocket;
        public Dictionary<string, string> extensions = new Dictionary<string, string>()
        {
            { "htm", "text/html" },
            { "html", "text/html" },
            { "xml", "text/xml" },
            { "txt", "text/plain" },
            { "css", "text/css" },
            { "png", "image/png" },
            { "gif", "image/gif" },
            { "jpg", "image/jpg" },
            { "jpeg", "image/jpeg" },
            { "zip", "application/zip"}
        };
        public Dictionary<string, string> header = new Dictionary<string, string>();
        public Response(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
        }
        private string GenerateHeader()
        {
            if (header.Count<1)
            {
                return null;
            }
            var items = from item in header
                        select item.Key + ": " + item.Value;
            return $"{string.Join("\r\n", items)}\r\n";

        }
        private void sendResponse(byte[] bContent, int responseCode, string contentType)
        {
            try
            {
                byte[] bHeader = charEncoder.GetBytes(
                                    "HTTP/1.1 " + responseCode.ToString() + "\r\n"
                                  + "Server: Micro Web Server\r\n"
                                  + "Content-Length: " + bContent.Length.ToString() + "\r\n"
                                  + "Connection: close\r\n"
                                  + GenerateHeader()
                                  + "Content-Type: " + contentType + "\r\n\r\n");
                clientSocket.Send(bHeader);
                clientSocket.Send(bContent);
                clientSocket.Close();
            }
            catch { }
        }
        public void send200Ok(string bContent, string contentType)
        {
            sendResponse(charEncoder.GetBytes(bContent), 200, contentType);
        }
        public void sendNotFound(string bContent, string contentType)
        {
            sendResponse(charEncoder.GetBytes(bContent), 404, contentType);
        }
        public void send(string bContent,int statusCode, string contentType)
        {
            sendResponse(charEncoder.GetBytes(bContent), statusCode, contentType);
        }
    }
}
