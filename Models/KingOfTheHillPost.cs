using Piranha;
using Piranha.AttributeBuilder;
using Piranha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Models;
using war3playground.BusinessLogic.Models.Interfaces;
using war3playground.BusinessLogic.Services.Interfaces;

namespace war3playground.Models
{
    [PostType(Title = "King Of The Hill post")]
    public class KingOfTheHillPost : Post<KingOfTheHillPost>, IW3Post
    {
        [Region]
        public Regions.Heading Heading { get; set; }

        public Player PlayerBlue { get; set; }

        public Player PlayerRed { get; set; }

        public void Init(IPlayerService playerService)
        {
        }
    }
}
