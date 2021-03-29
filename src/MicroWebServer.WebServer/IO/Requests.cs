using System.Collections.Generic;

namespace MicroWebServer.WebServer.IO
{
    public class Requests
    {
        public Dictionary<string, string> requestInfo { get; set; }
        public Dictionary<string, string> header { get; set; }
        public Dictionary<string, string> cookie { get; set; }
        public string body { get; set; }
        public Requests(string request)
        {
            string[] Info = request.Split("\n")[0].Split(" ");
            requestInfo = new Dictionary<string, string>()
            {
                {"method" ,Info[0]},
                {"path" ,Info[1]},
                {"httpVersion",Info[2]}
            };
            Splitter(request);
        }
        private void Splitter(string request)
        {
            header = new Dictionary<string, string>();
            cookie = new Dictionary<string, string>();
            string[] headerAndBody = request.Split("\r\n\r\n");
            if (requestInfo["method"] != "GET" && requestInfo["method"] != "DELETE")
            {
                body = headerAndBody[1];
            }
            string[] dataSplited = headerAndBody[0].Split("\n");
            for (int i = 1; i < dataSplited.Length; i++)
            {
                if (dataSplited[i].Contains(":"))
                {
                    string[] item = dataSplited[i].Split(":");
                    if (item[0] == "cookie")
                    {
                        cookie[item[1].Split("=")[0]] = item[1].Split("=")[1];
                    }
                    else
                    {
                        string[] segment = dataSplited[i].Split(":");
                        header[segment[0].Trim()] = segment[1].Trim();
                    }
                }
            }

        }
        public string getHeader(string key,string defaultValue)
        {
            if (header.ContainsKey(key))
            {
                return header[key];
            }
            return defaultValue;
        }
    }
}
