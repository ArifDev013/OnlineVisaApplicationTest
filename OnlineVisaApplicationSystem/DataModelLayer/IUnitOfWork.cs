using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>(Expression<Func<T, object>> idProperty) where T : class;
        void Save();
        Task SaveAsync();
        void DetachAll();
        void BackupDb(string path);
        void Restore(string path);

        void SetContext(DbContext dbContext);
    }
}
