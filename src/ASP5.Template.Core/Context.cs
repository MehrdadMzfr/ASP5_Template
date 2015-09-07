using System;
using System.Linq;
using ASP5.Template.Core.Entities;
using ASP5.Template.Core.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Internal;
using Microsoft.Framework.Configuration;

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
