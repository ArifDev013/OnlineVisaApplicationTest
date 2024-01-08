using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Attach(TEntity entityToAttach);
        void Delete(Expression<Func<TEntity, bool>> where);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        bool Exists(object primaryKey);
        IEnumerable<TEntity> Get();
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAsQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync();
        TEntity GetByID(object id);
        Task<TEntity> GetByIDAsync(object id);
        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetManyFunc(Func<TEntity, bool> where);
        Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        IQueryable<TEntity> GetManyQueryable(Expression<Func<TEntity, bool>> where);
        TEntity GetSingle(Func<TEntity, bool> predicate);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
