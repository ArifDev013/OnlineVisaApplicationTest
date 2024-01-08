using Audit.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [AuditDbContext(Mode = AuditOptionMode.OptIn, IncludeEntityObjects = true, AuditEventType = "{database}_{context}")]
    public abstract class DbContextBase: AuditDbContext
    {
        private readonly DbMigrationsConfiguration configuration;
        public DbContextBase(string conString, DbMigrationsConfiguration configuration) : base(conString)
        {
            this.configuration = configuration;
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
           // Database.SetInitializer<DbContextCombiner>(new CreateDatabaseIfNotExists<DbContextCombiner>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 6));
            base.OnModelCreating(modelBuilder);
            CreateModel(modelBuilder);
        }

        public abstract void CreateModel(DbModelBuilder modelBuilder);

        public void CreateDatabase()
        {
            var migrator = new DbMigrator(configuration);
            var migrations = migrator.GetPendingMigrations();
            foreach (var migration in migrations)
            {
                try
                {
                    migrator.Update(migration);
                }
                catch (Exception ex)
                { 
                    
                    throw new DbMigrationException($"Database Migration ({migration}) Failed\nMessage: {ex.Message}");
                }
            }
        }
    }

    [Serializable]
    internal class DbMigrationException : Exception
    {
        public DbMigrationException()
        {
        }

        public DbMigrationException(string message) : base(message)
        {
        }

        public DbMigrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbMigrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
