using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBITLABAPI.DataModel
{
    /// <summary>
    /// Extention Methods for Business Services
    /// </summary>
    public static class BusinessServiceExtentions
    {
        public static IEnumerable<TEntity> GetPage<TEntity>(this IQueryable<TEntity> queriable, Expression<Func<TEntity, object>> orderByExp,
            int pageLength, int page) where TEntity : class => queriable.OrderBy(orderByExp).Skip(page * pageLength).Take(pageLength).ToList();
    }
}
