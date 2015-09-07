using ASP5.Template.Core.Entities;
using ASP5.Template.Core.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;
using Microsoft.Framework.Configuration;

namespace ASP5.Template.Core
{
    public class Context: DbContext
    {
        //private string _connectionString;
        [FromServices]
        public ContextConfiguration Config { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Model> Models { get; set; }

        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Context;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
