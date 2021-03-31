using System.Collections.Generic;

namespace MicroWebServer.WebServer.IO
{
    public class Requests
    {
        public Dictionary<string, string> requestInfo { get; set; }
        public Dictionary<string, string> header = new Dictionary<string, string>();
        public Dictionary<string, string> cookie = new Dictionary<string, string>();
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
                    if (dataSplited[i].Split(":")[0] == "Cookie")
                    {
                        string[] item = dataSplited[i].Split(":");
                        if (item[1].Contains(";"))
                        {
                            item = item[1].Split(";");
                            foreach (var cookieItem in item)
                            {
                                cookie[cookieItem.Split("=")[0].Trim()] = cookieItem.Split("=")[1];
                            }
                        }
                        else
                        {
                            cookie[item[1].Split("=")[0].Trim()] = item[1].Split("=")[1];
                        }
                    }
                    else
                    {
                        string[] segment = dataSplited[i].Split(":");
                        header[segment[0].Trim().Trim()] = segment[1].Trim();
                    }
                }
            }

        }
        public string getHeader(string key,string defaultValue)
        {
            return header.ContainsKey(key) ? header[key] : defaultValue;
        }
        public string getCookie(string key, string defaultValue)
        {
            return cookie.ContainsKey(key) ? cookie[key] : defaultValue;
        }
        public string getArg(string key,string defaultValue)
        {
            if (requestInfo["path"].Contains('?'))
            {
                string allArgs = requestInfo["path"].Split('?')[1];
                if (allArgs.Contains('&'))
                {
                    string[] args = allArgs.Split('&');
                    foreach (var item in args)
                    {
                        if (item.Split('=')[0]==key)
                        {
                            return item.Split('=')[1];
                        }
                    }
                }
                if (allArgs.Split('=')[0] == key)
                {
                    return allArgs.Split('=')[1];
                }
            }
            return defaultValue;
        }
    }
}
