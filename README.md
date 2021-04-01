# Micro Web Server
A micro web server for a restful api
<br>
This project is for my associate thesis.
<br>
In this project, we have tried to design a simple tool so that programmers of other languages ​​such as Python can set up their web server with C #.
# Capabilities
Logging (console and syslog)
<br>
Middleware
<br>
&...
# Example
```csharp

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
        
static void Main(string[] args)
{
    ConsoleLog consoleLog = new ConsoleLog();
    Dictionary<string, Action<Requests, Response>> urlPatterns = new Dictionary<string, Action<Requests, Response>>()
    {
        {@"^\/info\?name\=[a-z]+\&age=\d+$", Info},
    };

    Server server = new Server(IPAddress.Parse("127.0.0.1"), 8080, 10, urlPatterns, consoleLog);
    if (server.Start())
    {
        consoleLog.Informational("Started");
    }
}
```
```
http://127.0.0.1:8080/info?name=HSNHK&age=19
```
