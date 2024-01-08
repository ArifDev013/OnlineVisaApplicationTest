using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IGenericCrudService<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);
        void Delete(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAsQueryable();
        TEntity GetById(object Id);
        IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetPage(int pageLength, int page, Expression<Func<TEntity, object>> orderByExp);
        IEnumerable<TEntity> GetPage(int pageLength, int page, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderByExp);
        TEntity Update(TEntity entity);
        T GetLastId<T>(Expression<Func<TEntity, bool>> where);
    }
}
