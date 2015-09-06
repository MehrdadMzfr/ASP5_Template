using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Builder.Internal;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Mvc;

namespace ASP5_Template_Web.Middleware
{
    public class ForceSsl
    {
        private readonly RequestDelegate _next;

        public ForceSsl(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.IsHttps)
            {
                var url = context.Request.GetDisplayUrl();
                //var redirectUrl = url.Replace("http", "https");
                var redirectUrl = "https://localhost:44301";
                context.Response.Redirect(redirectUrl);
                var task = Task.FromResult("Sorry not Secure...");
                await task;
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
