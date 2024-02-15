using System.Diagnostics.Eventing.Reader;
using System.Xml;
using FailedLoginsMonitor.BL.Interfaces;

namespace FailedLoginsMonitor.BL
{
    /// <summary>
    /// Implementation of parsing security events
    /// </summary>
    public class SecurityEventLogParser : ILogParser
    {
        public string Parse(EventRecord eventRecord)
        {
            var xml = eventRecord.ToXml();
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            var userNode = xmlDoc.SelectSingleNode("//EventData/Data[@Name='TargetUserName']");
            var ipAddressNode = xmlDoc.SelectSingleNode("//EventData/Data[@Name='IpAddress']");

            string userName = userNode?.InnerText;
            string ipAddress = ipAddressNode?.InnerText;

            return $"User: {userName}, IP Address: {ipAddress}";
        }
    }
}
