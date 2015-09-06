using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Builder.Internal;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Mvc;

namespace ASP5.Template.Web.Middleware
{
    public class ForceSsl
    {
        private readonly RequestDelegate _next;

        public ForceSsl(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.IsHttps)
                return _next.Invoke(context);
            var url = context.Request.GetEncodedUrl();
            var redirectUrl = url.Replace("http", "https");
            context.Response.Redirect(redirectUrl);
            return Task.FromResult(0);
        }
    }
}
