using Diary.Models.Wrappers;
using Diary.Properties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                var connectionString = string.Concat("Server=", Settings.Default.ServerName, @"\", Settings.Default.ServerInstance, ";Database=", Settings.Default.SQLDatabaseName, ";User Id=", Settings.Default.ServerUserName, ";Password=", Settings.Default.ServerUserPassword, ";");
                return connectionString;
            }
            set
            {
                SQLConnectionString = value;
            }
        }

    }
}
