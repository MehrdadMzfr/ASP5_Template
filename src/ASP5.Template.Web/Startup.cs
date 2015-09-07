using ASP5.Template.Core;
using ASP5.Template.Core.Models;
using ASP5.Template.Web.Middleware;
using ASP5.Template.Web.Models;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;


namespace ASP5.Template.Web
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
            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            services.AddInstance<IDataLayer>(new DataLayer(connectionString));
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddInstance<Context>(new Context());
            services.AddInstance<ContextConfiguration>(new ContextConfiguration {ConnectionString = connectionString});
        }

        public void Configure(IApplicationBuilder app)
        {
#if !DEBUG
            app.UseForceSSL();
#endif
            app.UseDefaultFiles();
            app.UseMvc(builder =>
            {
                //builder.MapRoute(name: "defaultRouter", template: "{controller=Home}/{action=Index}");
                builder.MapRoute(name: "defaultApi", template: "api/{controller}/{action}");
            });
            app.UseStaticFiles();
        }
    }
}
