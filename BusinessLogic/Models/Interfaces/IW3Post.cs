using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Services.Interfaces;

namespace war3playground.BusinessLogic.Models.Interfaces
{
    public interface IW3Post
    {
        void Init(IPlayerService playerService);
    }
}
