using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    /// <summary>
    /// Generic Business Service
    /// </summary>
    /// <typeparam name="TEntity">Entity Type of BusinessService</typeparam>
    public class GenericBusinessService<TEntity> : BusinessServiceBase, IGenericBusinessService<TEntity> where TEntity : class
    {
        protected readonly Expression<Func<TEntity, object>> idProperty;
        public GenericBusinessService(IUnitOfWork unitOfWork, Expression<Func<TEntity, object>> idProperty) : base(unitOfWork)
        {
            this.idProperty = idProperty;

        }

        /// <summary>
        /// Get Repository of type TEntity
        /// </summary>
        public IGenericRepository<TEntity> Repository => Repository<TEntity>(idProperty);
    }
}
