
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataModel;
using Utils;

namespace DataModel
{
    public   class AppDataContext: DbContextBase
    {
        public AppDataContext() : base(GlobalData.GetSqlServerConnetionWithPwd(), new Migrations.Configuration())
        {
        }

        public override void CreateModel(DbModelBuilder modelBuilder)
        {

            
            modelBuilder.Entity<VisaApplicant>();
            modelBuilder.Entity<VisaApplication>();
            modelBuilder.Entity<VisaType>();
            modelBuilder.Entity<Documents>();

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 6));
            base.OnModelCreating(modelBuilder);
            CreateModel(modelBuilder);
        }
    }
}
