using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rebex.Net;

namespace MicroWebServer.WebServer.Logging
{
    public class SysLog:ILog
    {
        private SyslogClient client;
        private SyslogMessage SyslogMessage;
        public SysLog(string HOST,int PORT)
        {
            client = new SyslogClient(SyslogTransportProtocol.Tcp, HOST, PORT);
            SyslogMessage = new SyslogMessage();
            SyslogMessage.Facility = SyslogFacilityLevel.User;
        }

        public void Alert(string Message)
        {
            SyslogMessage.Severity = SyslogSeverityLevel.Alert;
            SyslogMessage.Text = Message;
            client.Send(SyslogMessage);
        }

        public void Critical(string Message)
        {
            SyslogMessage.Severity = SyslogSeverityLevel.Critical;
            SyslogMessage.Text = Message;
            client.Send(SyslogMessage);
        }

        public void Debug(string Message)
        {
            SyslogMessage.Severity = SyslogSeverityLevel.Debug;
            SyslogMessage.Text = Message;
            client.Send(SyslogMessage);
        }

        public void Error(string Message)
        {
            SyslogMessage.Severity = SyslogSeverityLevel.Error;
            SyslogMessage.Text = Message;
            client.Send(SyslogMessage);
        }

        public void Informational(string Message)
        {
            SyslogMessage.Severity = SyslogSeverityLevel.Informational;
            SyslogMessage.Text = Message;
            client.Send(SyslogMessage);
        }

        public void Warning(string Message)
        {
            SyslogMessage.Severity = SyslogSeverityLevel.Critical;
            SyslogMessage.Text = Message;
            client.Send(SyslogMessage);
        }
    }
}
