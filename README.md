# Micro Web Server
A micro web server for a restful api and etc...
<br>
This project is for my associate thesis.
<br>
In this project, we have tried to design a simple tool so that programmers of other languages ​​such as Python can set up their web server with C #.
# Capabilities
Logging (console and syslog)
<br>
Middleware
<br>
Fast and Secure
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
# Request
Request Information:
```csharp
//request method (GET,POST,PUT,DELETE,...)
request.requestInfo["method"]

//request full path
request.requestInfo["path"]

//request http version
request.requestInfo["httpVersion"]

```
Headers:
```csharp
//get header value with key
request.getHeader(key,defaultValue)
```
Cookies:
```csharp
//get cookie value with key
request.getCookie(key,defaultValue)
```
URL Parameters:
```csharp
//get URL parameter with key
request.getArg(key,defaultValue)
```
Authorization:
```csharp
request.getAuthHeader() -> {"type","key"}
```
Body:
```csharp
//Receive body value as a string
request.body
```
# Response
Headers:
```csharp
//set header
response.header["key"]="value"
```
Cookies:
```csharp
//set cookie
response.cookie["key"]="value"
```
Content type and Status code:
```csharp
response.extensions["name"]
response.statusCode["code"]
```
Send response:
```csharp
response.send200Ok(Content , contentType)
response.send(Content , statusCode , contentType)
response.sendJson(Content , statusCode)
response.sendNotFound(Content , contentType)
```
Security:
```csharp
//Set xss protection header
response.setSecurityHeader()
//The text generates a secure response
response.safeResponse(response)
```
Redirect:
```csharp
//redirect to path
response.redirect("/path");
```
# Middleware
An example of setting up a middleware:
```csharp
public static (Requests,Response) AccessControllMiddleware(Requests requests, Response response)
{
    response.header["Access-Control-Allow-Origin"] = "*";
    response.header["Access-Control-Allow-Headers"] = "Content-Type, Content-Length, Accept-Encoding";
    return (requests, response);
}

...

server.Middlewares.Add(AccessControlMiddleware);
server.Start();
```
