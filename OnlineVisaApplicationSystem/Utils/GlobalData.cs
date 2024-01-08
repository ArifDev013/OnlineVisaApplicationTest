using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
 public  static class GlobalData
	{

		private static string instanceName = @"ARF\SQLEXPRESS";
		public static string InstanceName
		{
			get { return instanceName; }
			set { instanceName = value; }
		}
		private static string databaseName = "visaDb";
		public static string DatabaseName
		{
			get { return databaseName; }
			set { databaseName = value; }
		}
		public static Settings LocalSettings { get; set; } = new Settings();
		public static string GetSqlServerConnetionWithPwd()
		{

			return $@"Data Source={InstanceName};Initial Catalog={DatabaseName}; Integrated Security=False;User ID={LocalSettings.Uid};Password={LocalSettings.Password};MultipleActiveResultSets=true;";

		}
		public static string GetSqlServerMasterConnetionWithPwd(string instance)
        {

            return $@"Data Source={instance};Initial Catalog=Master; Integrated Security=False;User ID={LocalSettings.Uid};Password={LocalSettings.Password};MultipleActiveResultSets=true;";

        }
    }
}
