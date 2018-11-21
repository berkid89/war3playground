using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using war3playground.BusinessLogic.DatabaseContexts;
using war3playground.BusinessLogic.Extensions;
using war3playground.BusinessLogic.Models;
using war3playground.BusinessLogic.Services.Interfaces;
using war3playground.BusinessLogic.Settings;

namespace war3playground.BusinessLogic.Services
{
    public class PlayerService : ContextServiceBase, IPlayerService
    {
        public PlayerService(ISettings settings, ILogger logger, W3PContext db) : base(settings, logger, db)
        {
        }

        public IQueryable<Player> List(params string[] includes)
        {
            return db.Players.HandleIncludes(includes);
        }
    }
}
