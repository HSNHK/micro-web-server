using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MicroWebServer.WebServer.IO
{
    class Response
    {
        private Encoding charEncoder = Encoding.UTF8;
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
        private void sendResponse(Socket clientSocket, byte[] bContent, int responseCode, string contentType)
        {
            try
            {
                byte[] bHeader = charEncoder.GetBytes(
                                    "HTTP/1.1 " + responseCode.ToString() + "\r\n"
                                  + "Server: Micro Web Server\r\n"
                                  + "Content-Length: " + bContent.Length.ToString() + "\r\n"
                                  + "Connection: close\r\n"
                                  + "Content-Type: " + contentType + "\r\n\r\n");
                clientSocket.Send(bHeader);
                clientSocket.Send(bContent);
                clientSocket.Close();
            }
            catch { }
        }
        public void send200Ok(Socket clientSocket, string bContent, string contentType)
        {
            sendResponse(clientSocket, charEncoder.GetBytes(bContent), 200, contentType);
        }
        public void sendNotFound(Socket clientSocket, string bContent, string contentType)
        {
            sendResponse(clientSocket, charEncoder.GetBytes(bContent), 404, contentType);
        }
        public void send(Socket clientSocket, string bContent,int statusCode, string contentType)
        {
            sendResponse(clientSocket, charEncoder.GetBytes(bContent), statusCode, contentType);
        }
    }
}
