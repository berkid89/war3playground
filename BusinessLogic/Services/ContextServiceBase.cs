using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.DatabaseContexts;
using war3playground.BusinessLogic.Services.Interfaces;
using war3playground.BusinessLogic.Settings;

namespace war3playground.BusinessLogic.Services
{
    public abstract class ContextServiceBase : ServiceBase, IContextService
    {
        protected readonly W3PContext db;

        public ContextServiceBase(ISettings settings, ILogger logger, W3PContext db) : base(settings, logger)
        {
            this.db = db;
        }

        public virtual int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
