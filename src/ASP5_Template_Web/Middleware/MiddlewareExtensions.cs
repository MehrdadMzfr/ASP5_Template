using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;

namespace ASP5_Template_Web.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseForceSSL(this IApplicationBuilder app, Action<IApplicationBuilder> configuration)
        {
            var laterApp = app.UseMiddleware<ForceSsl>();
            configuration(laterApp);
            return laterApp;
        }
    }
}
