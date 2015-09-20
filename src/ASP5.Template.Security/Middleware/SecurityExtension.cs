using System;
using System.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Cryptography;
using AspNet.Security.OpenIdConnect.Server;
using ASP5.Template.Security.Providers;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;

namespace ASP5.Template.Security.Middleware
{
    public static class SecurityExtension
    {
        public static IApplicationBuilder UseForceSSL(this IApplicationBuilder app)
        {
            var laterApp = app.UseMiddleware<ForceSsl>();
            return laterApp;
        }

        public static IApplicationBuilder UseProtectedDirectory(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthorizeDirectoryAccess>();
        }

        public static IApplicationBuilder UseSecurity(this IApplicationBuilder app, SecurityOptions securityOptions)
        {
            app.UseOAuthBearerAuthentication(options =>
            {
                options.AutomaticAuthentication = true;
                options.Authority = securityOptions.Authority;
                    //TODO: avoid errors in the futre? github.com/aspnet-contrib/AspNet.Security.OpenIdConnect.Server/issues/94#issuecomment-118359248
                    options.TokenValidationParameters.ValidateAudience = false;
            });

            app.UseOpenIdConnectServer(options =>
            {
                options.AuthenticationScheme = OpenIdConnectDefaults.AuthenticationScheme;

                if (!securityOptions.IsMonoEnvironment)
                {
                    var rsaCryptoServiceProvider = new RSACryptoServiceProvider(2048);
                    var rsaParameters = rsaCryptoServiceProvider.ExportParameters(true);

                    options.UseKey(new RsaSecurityKey(rsaParameters));
                }
                else
                {
                    var temp = typeof(IApplicationBuilder).GetTypeInfo().Assembly;
                    options.UseCertificate(typeof(IApplicationBuilder).GetTypeInfo().Assembly, "ASP5.Template.Web.Certificate.pfx", "Owin.Security.OpenIdConnect.Server");
                }

                options.ApplicationCanDisplayErrors = true;
                options.AllowInsecureHttp = true;
                options.Issuer = new Uri(securityOptions.Authority);
                options.TokenEndpointPath = new PathString("/token");
                options.AuthorizationEndpointPath = PathString.Empty;
                options.AccessTokenLifetime = securityOptions.TokenLifeTime;
                options.ValidationEndpointPath = PathString.Empty;
                options.Provider = new DefaultAuthorizationProvider();
            });
            return app;
        }
    }
}
