using ASP5.Template.Core.Entities;
using Microsoft.Data.Entity;

namespace ASP5.Template.Core
{
    public class Context : DbContext
    {
        public static string ConnectionString;
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }

        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
