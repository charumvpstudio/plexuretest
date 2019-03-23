using System;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;

namespace PracticeTest.Logging
{
    /// <summary>
    /// Interface that must be implemented by all classes that process 
    /// log events.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an event.
        /// </summary>
        /// <param name="eventLog">The log event to process.</param>
        [OneWay]
        void Log(EventLog eventLog);
    }
}
