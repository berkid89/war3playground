using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using war3playground.BusinessLogic.Models;

namespace war3playground.BusinessLogic.DatabaseContexts
{
    public class W3PContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public W3PContext(DbContextOptions<W3PContext> options) : base(options)
        {
        }
    }
}
