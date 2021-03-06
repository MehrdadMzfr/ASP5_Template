﻿using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.DependencyInjection;

namespace ASP5.Template.Core
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Context;Trusted_Connection=True;";
            services.AddEntityFramework().AddSqlServer().AddDbContext<Context>(options => options.UseSqlServer(connectionString));
        }
    }
}
