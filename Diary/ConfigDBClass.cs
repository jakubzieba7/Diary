using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class ConfigDBClass : DbConfiguration
    {
        //public ConfigDBClass() 
        //{
        //    SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
        //    SetDefaultConnectionFactory(new SqlConnectionFactory());
        //}

        public string SQLConnectionString { get; set; }
    }
}
