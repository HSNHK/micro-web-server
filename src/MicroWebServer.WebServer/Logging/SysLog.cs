using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rebex.Net;

namespace MicroWebServer.WebServer.Logging
{
    class SysLog:ILog
    {
        private SyslogClient client;
        private SyslogMessage SyslogMessage;
        public SysLog(string HOST,int PORT)
        {
            client = new SyslogClient(SyslogTransportProtocol.Tcp, HOST, PORT);
        }

        public async Task Alert(string Message)
        {
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
            SyslogMessage.Severity = SyslogSeverityLevel.Alert;
            SyslogMessage.Text = Message;
            await client.SendAsync(SyslogMessage);
        }

        public async Task Critical(string Message)
        {
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
            SyslogMessage.Severity = SyslogSeverityLevel.Critical;
            SyslogMessage.Text = Message;
            await client.SendAsync(SyslogMessage);
        }

        public async Task Debug(string Message)
        {
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
            SyslogMessage.Severity = SyslogSeverityLevel.Debug;
            SyslogMessage.Text = Message;
            await client.SendAsync(SyslogMessage);
        }

        public async Task Error(string Message)
        {
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
            SyslogMessage.Severity = SyslogSeverityLevel.Error;
            SyslogMessage.Text = Message;
            await client.SendAsync(SyslogMessage);
        }

        public async Task Informational(string Message)
        {
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
            SyslogMessage.Severity = SyslogSeverityLevel.Informational;
            SyslogMessage.Text = Message;
            await client.SendAsync(SyslogMessage);
        }

        public async Task Warning(string Message)
        {
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
            SyslogMessage.Severity = SyslogSeverityLevel.Critical;
            SyslogMessage.Text = Message;
            await client.SendAsync(SyslogMessage);
        }
    }
}
