using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MicroWebServer.WebServer.IO;
namespace MicroWebServer.WebServer.Middleware
{
    class InternalMiddleware
    {
        public (Requests,Response) TimeHeader(Requests requests,Response response)
        {
            response.header["time"] = DateTime.Now.ToString();
            return (requests,response);
        }
    }
}
