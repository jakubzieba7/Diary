using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Models.Domains
{
    public class SQLSettings
    {
        public string ServerName { get; set; }
        public string ServerInstance { get; set; }
        public string SQLDatabaseName { get; set; }
        public string ServerUserName { get; set; }
        public string ServerUserPassword { get; set; }


    }
}
