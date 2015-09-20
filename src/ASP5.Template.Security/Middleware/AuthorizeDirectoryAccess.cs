using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Extensions;

namespace ASP5.Template.Security.Middleware
{
    public class AuthorizeDirectoryAccess
    {
        private readonly RequestDelegate _next;
        private readonly PathString protectedPath = new PathString("/protected");

        public AuthorizeDirectoryAccess(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            var temp = context.User.Identities.ToList();
            if (!context.Request.Path.StartsWithSegments(protectedPath)) return _next(context);
            if (context.User.Identities.Any(identity => identity.IsAuthenticated)) return _next(context);
            context.Response.StatusCode = 401;
            return Task.FromResult(0);
        }
    }
}
