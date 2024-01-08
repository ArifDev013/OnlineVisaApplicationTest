using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch
            {
                DetachAll();
                throw;
            }
        }

        /// <summary>
        /// Detaches all of the DbEntityEntry objects that have been added to the ChangeTracker.
        /// </summary>
        public void DetachAll()
        {
            foreach (var dbEntityEntry in context.ChangeTracker.Entries())
            {
                try
                {
                    if (dbEntityEntry.Entity != null)
                    {
                        dbEntityEntry.State = EntityState.Detached;
                    }
                }
                catch { }
            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<T> GetRepository<T>(Expression<Func<T, object>> idProperty) where T : class
        {
            if (!repositories.ContainsKey(typeof(T)))
            {
                var repo = new GenericRepository<T>(context, idProperty);
                repositories.Add(typeof(T), repo);
            }
            return repositories[typeof(T)] as IGenericRepository<T>;
        }

        public void BackupDb(string path)
        {
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, $@"BACKUP DATABASE [{context.Database.Connection.Database}] TO  DISK = N'{path}' WITH NOFORMAT, NOINIT,  NAME = N'Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10");//BACKUP [{context.Database.Connection.Database}] \nTO DISK = '{path}' \nGO ");
        }

        public void Restore(string path)
        {
            //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, $"RESTORE DATABASE [{context.Database.Connection.Database}] FROM DISK = '{path}'");
            string restoreQuery = @"USE [Master]; 
                                                ALTER DATABASE ""{0}"" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                                RESTORE DATABASE ""{0}"" FROM DISK='{1}' WITH REPLACE;
                                                ALTER DATABASE ""{0}"" SET MULTI_USER;";
            restoreQuery = string.Format(restoreQuery, context.Database.Connection.Database, path);
            context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, restoreQuery);

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SetContext(DbContext context)
        {
            repositories.Clear();
            this.context.Dispose();
            this.context = context;
        }
    }
}
