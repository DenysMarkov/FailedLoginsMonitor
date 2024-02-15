using System.Diagnostics.Eventing.Reader;

namespace FailedLoginsMonitor.BL.Interfaces
{
    /// <summary>
    /// Interface for parsing log events
    /// </summary>
    public interface ILogParser
    {
        string Parse(EventRecord eventRecord);
    }
}
