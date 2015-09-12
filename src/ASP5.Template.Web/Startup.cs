﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using AspNet.Security.OpenIdConnect.Server;
using ASP5.Template.Core;
using ASP5.Template.Web.Models;
using ASP5.Template.Web.Middleware;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using System.Reflection;
using ASP5.Template.Core.Providers;

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
            services.AddInstance<IDataLayer>(new DataLayer(connectionString));
            services.AddTransient<IBusinessService, BusinessService>();
            services.AddTransient<EfBusinessLayer>();
            Context.ConnectionString = connectionString;
            services.AddAuthentication();
            services.AddCaching();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IRuntimeEnvironment env)
        {
#if !DEBUG
            app.UseForceSSL();
#endif
            app.Map("/api", api =>
            {
                api.UseOAuthBearerAuthentication(options =>
                {
                    options.AutomaticAuthentication = true;
                    //options.SecurityTokenValidators = new List<ISecurityTokenValidator> { new };
                    options.Authority = "http://localhost:59849/";
                    options.Audience = "http://localhost:59849/";
                });
                api.UseMvc(builder =>
                {
                    builder.MapRoute(name: "defaultApi", template: "{controller}/{action}");
                });
            });

            app.UseOpenIdConnectServer(options =>
            {
                options.AuthenticationScheme = OpenIdConnectDefaults.AuthenticationScheme;

                if (string.Equals(env.RuntimeType, "Mono", StringComparison.OrdinalIgnoreCase))
                {
                    var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
                    var rsaParameters = rsaCryptoServiceProvider.ExportParameters(true);

                    options.UseKey(new RsaSecurityKey(rsaParameters));
                }
                else
                {
                    var temp = typeof(Startup).GetTypeInfo().Assembly;
                    options.UseCertificate(typeof(Startup).GetTypeInfo().Assembly, "ASP5.Template.Web.Certificate.pfx", "Owin.Security.OpenIdConnect.Server");
                }

                options.ApplicationCanDisplayErrors = true;
                options.AllowInsecureHttp = true;
                options.Issuer = new Uri("http://localhost:59849");
                options.TokenEndpointPath = new PathString("/token");
                //options.AuthorizationEndpointPath = new PathString("/token");
                options.AccessTokenLifetime = new TimeSpan(0, 15, 0);
                options.ValidationEndpointPath = PathString.Empty;
                options.Provider = new DefaultAuthorizationProvider();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
