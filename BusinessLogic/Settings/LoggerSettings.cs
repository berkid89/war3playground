using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace war3playground.BusinessLogic.Settings
{
    public class LoggerSettings
    {
        public LogEventLevel LogLevel { get; set; }
        public ColumnOptions ColumnOptions { get; set; }
    }
}
