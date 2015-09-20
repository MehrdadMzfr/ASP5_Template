using System;
using ASP5.Template.Core;
using ASP5.Template.Web.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using ASP5.Template.Security;
using ASP5.Template.Security.Middleware;

namespace ASP5.Template.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json").AddEnvironmentVariables()
                .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            Context.ConnectionString = connectionString;
            services.AddInstance<IDataLayer>(new DataLayer(connectionString));
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<EfBusinessLayer>();
            //services.AddAuthentication();
            services.AddCaching();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IRuntimeEnvironment env)
        {
#if !DEBUG
            app.UseForceSSL();
#endif
            var securityOptions = new SecurityOptions
            {
                Authority = Configuration["Data:TokenAuthority:http"],
                IsMonoEnvironment = string.Equals(env.RuntimeType, "Mono", StringComparison.OrdinalIgnoreCase),
                TokenLifeTime = TimeSpan.FromMinutes(10)
            };
            app.UseSecurity(securityOptions);
            app.UseProtectedDirectory();
            app.UseMvc(builder =>
            {
                builder.MapRoute(name: "defaultApi", template: "api/{controller}/{action}");
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
