using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ContextHelpers
{
    public static class DbContextExtensions
    {
        public static T CreateIfNotExist<T>(this DbContext context, Expression<Func<T, bool>> where, Func<T> createMethod) where T : class
           => context.Set<T>().FirstOrDefault(where) ?? context.Set<T>().Add(createMethod());
        /// <summary>
        /// Create Index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="context"></param>
        /// <param name="indexnam"></param>
        /// <param name="indexname"></param>
        public static void CreateIndex(this DbContext context, string index,string indexname, string tablename)
        {
              if (context.Database.SqlQuery<int>("SELECT  count(*) FROM sys.indexes WHERE name = @indexname AND object_id = OBJECT_ID(@tablename)", new object[] { new SqlParameter("@indexname", indexname), new SqlParameter("@tablename", tablename) }

                  ).Single() == 0)
                {

                    context.Database.ExecuteSqlCommand($"{index}");
                }
                else
                {
                    context.Database.ExecuteSqlCommand($"DROP INDEX {tablename}.{indexname}");
                    context.Database.ExecuteSqlCommand($"{index}");
                }
            
           
        }
        /// <summary>
        /// To Create or Alter Stored Procedure
        /// </summary>
        /// <param name="context"> Current Context</param>
        /// <param name="sp">StoredProcedure</param>
        /// <param name="spName">Sp Name</param>
        public static void CreateSP(this DbContext context, string sp, string spName)
        {
            if (context.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.objects WHERE type = 'P' AND name = @uspName",
              new SqlParameter("@uspName", spName)).Single() == 0)
            {

                context.Database.ExecuteSqlCommand($"Create  {sp}");
            }
            else
            {
                context.Database.ExecuteSqlCommand($"Alter  {sp}");
            }
        }
        /// <summary>
        /// To Create or Alter SQL Function
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fn">SQL Function</param>
        /// <param name="fnName">FN Name </param>
        public static void CreateFN(this DbContext context, string fn, string fnName)
        {
            if (context.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.objects WHERE type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ) AND name = @uspName",
              new SqlParameter("@uspName", fnName)).Single() == 0)
            {

                context.Database.ExecuteSqlCommand($"Create  {fn}");
            }
            else
            {
                context.Database.ExecuteSqlCommand($"Alter  {fn}");
            }
        }
        public static void CreateTrigger(this DbContext context, string tr, string TriggerName)
        {
            if (context.Database.SqlQuery<int>("SELECT COUNT(*) FROM sys.objects WHERE [name] = @uspName AND [type] = 'TR'",
              new SqlParameter("@uspName", TriggerName)).Single() == 0)
            {

                context.Database.ExecuteSqlCommand($"Create  {tr}");
            }
            else
            {
                context.Database.ExecuteSqlCommand($"Alter  {tr}");
            }
        }
    }
}
