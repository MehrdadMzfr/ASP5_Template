﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;

namespace ASP5_Template_Web.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseForceSSL(this IApplicationBuilder app)
        {
            var laterApp = app.UseMiddleware<ForceSsl>();
            return laterApp;
        }
    }
}