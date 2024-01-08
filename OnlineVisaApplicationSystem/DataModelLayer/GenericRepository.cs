using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity">Entity Type</typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;
        private readonly Func<TEntity, object> idProperty;
        public GenericRepository(DbContext context, Expression<Func<TEntity, object>> idProperty)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
            this.idProperty = idProperty.Compile();
        }

        /// <summary>
        /// Generic Get method for Entities
        /// </summary>
        /// <returns>All Entities</returns>
        public virtual IEnumerable<TEntity> Get()
        {
            return DbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Generic Async Get method for Entities
        /// </summary>
        /// <returns>All Entities</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// Generic async get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        /// <summary>
        /// Generic Insert method for the entities
        /// </summary>
        /// <param name="entity">Entity to Add</param>
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id">Id of the Entity to delete</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            if (entityToDelete != null)
                DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete">Entity to Delete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            entityToDelete = DbSet.Find(idProperty(entityToDelete));
            if (entityToDelete != null)
                DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate">Entity to Update</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            var originalEntity = GetByID(idProperty(entityToUpdate));
            var attachedEntry = Context.Entry(originalEntity);
            attachedEntry.CurrentValues.SetValues(entityToUpdate);
            attachedEntry.State = EntityState.Modified;
            Context.SaveChanges();
            //Context.Entry(originalEntity).State = EntityState.Detached;
            //if(originalEntity != entityToUpdate)
            //    originalEntity.Map(entityToUpdate);
            //Context.Entry(originalEntity).State = EntityState.Modified;

        }

        ///// <summary>
        ///// Detaches an entity
        ///// </summary>
        ///// <param name="entitytoDetach">Entity to Detach</param>
        //public virtual void Detach(TEntity entitytoDetach)
        //{
        //    Context.Entry(entitytoDetach).State = EntityState.Detached;
        //}

        /// <summary>
        /// Attach entity
        /// </summary>
        /// <param name="entityToAttach">Entity to Attach</param>
        public virtual void Attach(TEntity entityToAttach)
        {
            DbSet.Attach(entityToAttach);
        }

        /// <summary>
        /// Generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <returns>All Entities queried</returns>
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where).ToList();
        }

        public virtual IEnumerable<TEntity> GetManyFunc(Func<TEntity, bool> where)
        {
            return DbSet.AsNoTracking().ToList().Where(where).ToList();
        }

        /// <summary>
        /// Generic async method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <returns>All Entities queried</returns>
        public virtual async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await DbSet.AsNoTracking().Where(where).AsQueryable().ToListAsync();
        }

        /// <summary>
        /// Generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <returns>All Entities queried</returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.AsNoTracking().Where(where).AsQueryable();
        }

        /// <summary>
        /// Generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <returns>Entity queried</returns>
        public TEntity Get(Expression<Func<TEntity, Boolean>> where)
        {
            return DbSet.AsNoTracking().Where(where).FirstOrDefault<TEntity>();
        }

        /// <summary>
        /// Generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where">Predicate</param>
        /// <returns></returns>
        public void Delete(Expression<Func<TEntity, Boolean>> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            DbSet.RemoveRange(objects);
        }

        /// <summary>
        /// Generic method to fetch all the records from db
        /// </summary>
        /// <returns>All records</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        /// <summary>
        /// Generic async method to fetch all the records from db
        /// </summary>
        /// <returns>All records</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Generic method to fetch all the records from db as queryable
        /// </summary>
        /// <returns>All records</returns>
        public virtual IQueryable<TEntity> GetAllAsQueryable()
        {
            return DbSet.AsNoTracking().AsQueryable();
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey">primarykey to match</param>
        /// <returns>true if exists</returns>
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().Single<TEntity>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().FirstOrDefault<TEntity>(predicate);
        }
    }
}
