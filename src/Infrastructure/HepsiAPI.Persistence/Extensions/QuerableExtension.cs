using HepsiAPI.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HepsiAPI.Persistence.Extensions
{
    public static class QuerableExtension
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class, IBaseEntity
        {
            if (includes is not null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
