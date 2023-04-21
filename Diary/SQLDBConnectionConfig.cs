using Diary.Models;
using Diary.Properties;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;

namespace Diary
{
    public class SQLDBConnectionConfig : DbConfiguration
    {
        public SQLDBConnectionConfig()
        {
            SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
            SetDefaultConnectionFactory(new SqlConnectionFactory());
        }
        public string SQLConnectionString
        {
            get
            {
                var connectionString = string.Concat("Server=", Settings.Default.ServerName, @"\", Settings.Default.ServerInstance, "; Database=", Settings.Default.SQLDatabaseName, "; User Id=", Settings.Default.ServerUserName, ";Password=", Settings.Default.ServerUserPassword, ";");
                return connectionString;
            }
            set
            {
                SQLConnectionString = value;
            }
        }

    }
}
