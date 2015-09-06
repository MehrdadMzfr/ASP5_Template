using ASP5_Template_Web.Middleware;
using ASP5_Template_Web.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;

namespace ASP5_Template_Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath).AddJsonFile("config.json").AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.Configure<MvcOptions>(options => options.Filters.Add(new RequireHttpsAttribute()));
            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            services.AddInstance<IDataLayer>(new DataLayer(connectionString));
            services.AddTransient<IBusinessService, BusinessService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
#if !DEBUG
            app.UseForceSSL(innerApp => innerApp.UseStaticFiles());
#endif
            app.UseMvc(builder =>
            {
                //builder.MapRoute(name: "defaultRouter", template: "{controller=Home}/{action=Index}");
                builder.MapRoute(name: "defaultApi", template: "api/{controller}/{action}");
            });
            //app.UseStaticFiles();
        }
    }
}
