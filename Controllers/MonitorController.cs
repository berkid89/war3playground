using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Squire.Monitoring.Controllers;
using war3playground.BusinessLogic.Settings;

namespace war3playground.Controllers
{
    public class MonitorController : MonitoringController
    {
        private readonly ISettings settings;

        public MonitorController(ISettings settings)
        {
            this.settings = settings;
        }

        public override async Task<bool> HealthCheck()
        {
            try
            {
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        [HttpGet("serviceinfo")]
        public IActionResult ServiceInfo()
        {
            return Json(new
            {
                version = Program.GetVersion,
                env = settings.ENV.ToString(),
                logLevel = settings.Logging.LogLevel.ToString()
            });
        }
    }
}