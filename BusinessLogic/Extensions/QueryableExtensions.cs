using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace war3playground.BusinessLogic.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> HandleIncludes<TEntity>(this DbSet<TEntity> dbset, params string[] includes) where TEntity : class
        {
            var query = dbset.AsQueryable();

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}
