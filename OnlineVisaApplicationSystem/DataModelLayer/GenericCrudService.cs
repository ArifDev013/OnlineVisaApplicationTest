using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    /// <summary>
    /// Base class for CRUD Business services
    /// </summary>
    /// <typeparam name="TEntity">Entity of the service</typeparam>
    public class GenericCrudService<TEntity> : GenericBusinessService<TEntity>, IGenericCrudService<TEntity> where TEntity : class
    {
        public GenericCrudService(IUnitOfWork unitOfWork, Expression<Func<TEntity, object>> idProperty) : base(unitOfWork, idProperty)
        {
           
        }
        /// <summary>
        /// Returns All Items
        /// </summary>
        /// <returns>All Entity</returns>
        public IQueryable<TEntity> GetAllAsQueryable() => Repository.GetAllAsQueryable();
        /// <summary>
        /// Returns All Items
        /// </summary>
        /// <returns>All Entity</returns>
        public IEnumerable<TEntity> GetAll() => Repository.GetAll();

        /// <summary>
        /// Returns All Items filtered by a condiont
        /// </summary>
        /// <param name="where">Condition To Filter</param>
        /// <returns>All Item Filtered</returns>
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where) => Repository.GetMany(where);

        /// <summary>
        /// Returns An Entity filetered by filter
        /// </summary>
        /// <param name="where">Filter Condition</param>
        /// <returns>Filtered Entity</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> where) => Repository.Get(where);

        /// <summary>
        /// Get A Page of Entities
        /// </summary>
        /// <param name="pageLength">Length of the page</param>
        /// <param name="page">Page Number: starts with zero</param>
        /// /// <param name="orderByExp">Order by clause</param>
        /// <returns>Page</returns>
        public IEnumerable<TEntity> GetPage(int pageLength, int page, Expression<Func<TEntity, object>> orderByExp) =>
            Repository.GetAllAsQueryable().GetPage(orderByExp, pageLength, page);

        /// <summary>
        /// Get A Page of Entities
        /// </summary>
        /// <param name="pageLength">Length of the page</param>
        /// <param name="page">Page Number: starts with zero</param>
        /// /// <param name="orderByExp">Order by clause</param>
        /// <returns>Page</returns>
        public IEnumerable<TEntity> GetPage(int pageLength, int page, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderByExp) =>
            Repository.GetManyQueryable(where).GetPage(orderByExp, pageLength, page).ToList();

        /// <summary>
        /// Returns an Entity by Id
        /// </summary>
        /// <param name="Id">Id of the Entity</param>
        /// <returns>Return Entity of the specified Id</returns>
        public TEntity GetById(object Id) => Repository.GetByID(Id);

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity">Entity to Add</param>
        /// <returns>Return Entity added</returns>
        public virtual TEntity Add(TEntity entity)
        {
            
            SaveUnderTransaction(() => Repository.Insert(entity));
            return entity;
        }

        /// <summary>
        /// Updates an Entity
        /// </summary>
        /// <param name="entity">Entity to Update</param>
        /// <returns>Return Entity Updated</returns>
        public virtual TEntity Update(TEntity entity)
        {
            SaveUnderTransaction(() => Repository.Update(entity));
            return entity;
        }

      
        /// <summary>
        /// Deletes an Entity
        /// </summary>
        /// <param name="entity">Entity to Delete</param>
        public virtual void Delete(TEntity entity) => SaveUnderTransaction(() => Repository.Delete(entity));


        public T GetLastId<T>(Expression<Func<TEntity, bool>> where)
        {
            var lastEntity = Repository.GetMany(where)?.LastOrDefault();
            if (lastEntity == null)
                return default(T);
            return (T)idProperty.Compile()(lastEntity);
        }
    }
}

