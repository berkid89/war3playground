using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Enums;

namespace war3playground.BusinessLogic.Settings
{
    public interface ISettings
    {
        ServiceEnv ENV { get; }

        string ConnectionString { get; }

        LoggerSettings Logging { get; }

        int PerformaceWarningMinimumInMS { get; }

        string ClientApiKey { get; }
    }
}
