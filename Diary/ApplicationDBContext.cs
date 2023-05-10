using Diary.Models.Configurations;
using Diary.Models.Domains;
using Diary.Properties;
using System.Data.Entity;

namespace Diary
{
    public class ApplicationDBContext : DbContext
    {
        private static string _sqlConnectionString = string.Concat(@"Server=.\SIGMANEST;Database=DIARY;User Id=DOTNET;Password=DOTNET;");
        public ApplicationDBContext() : base(string.Concat("Server=", Settings.Default.ServerName, @"\", Settings.Default.ServerInstance, "; Database=", Settings.Default.SQLDatabaseName, "; User Id=", Settings.Default.ServerUserName, ";Password=", Settings.Default.ServerUserPassword, ";"))
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
        }

    }

}