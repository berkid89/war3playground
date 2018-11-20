using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Enums;

namespace war3playground.BusinessLogic.Settings
{
    public class Settings : ISettings
    {
        public ServiceEnv ENV { get; }

        public string ConnectionString { get; }

        public LoggerSettings Logging { get; }

        public int PerformaceWarningMinimumInMS { get; }

        public string ClientApiKey { get; }

        public Settings(IConfiguration config)
        {
            ENV = (ServiceEnv)Enum.Parse(typeof(ServiceEnv), config["ENV"]);

            ConnectionString = config["ConnectionString"];

            var columnOptions = new ColumnOptions();
            columnOptions.Store.Remove(StandardColumn.LogEvent);
            columnOptions.Store.Remove(StandardColumn.Properties);
            columnOptions.Store.Remove(StandardColumn.MessageTemplate);

            Logging = new LoggerSettings()
            {
                LogLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), config["Logging:LogLevel:Default"]),
                ColumnOptions = columnOptions
            };

            PerformaceWarningMinimumInMS = Convert.ToInt32(config["PerformaceWarningMinimumInMS"]);
        }
    }
}
