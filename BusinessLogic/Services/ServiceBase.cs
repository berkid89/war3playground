using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Settings;

namespace war3playground.BusinessLogic.Services
{
    public abstract class ServiceBase
    {
        protected readonly ILogger logger;
        protected readonly ISettings settings;

        public ServiceBase(ISettings settings, ILogger logger)
        {
            this.settings = settings;
            this.logger = logger;
        }
    }
}
