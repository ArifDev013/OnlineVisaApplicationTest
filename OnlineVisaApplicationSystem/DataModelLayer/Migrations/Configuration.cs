using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
namespace DataModel.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<DataModel.AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataModel.AppDataContext context)
        {
        }

    }
}
