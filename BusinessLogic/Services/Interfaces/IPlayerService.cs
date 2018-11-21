using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Models;

namespace war3playground.BusinessLogic.Services.Interfaces
{
    public interface IPlayerService : IContextService
    {
        IQueryable<Player> List(params string[] includes);
    }
}
