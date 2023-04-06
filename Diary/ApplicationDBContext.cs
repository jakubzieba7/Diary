using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
            : base("name=ApplicationDBContext")
        {
        }

    }

}