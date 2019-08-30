using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace HWL.API.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await next.Invoke(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.Redirect("/document/index");
            }
        }
    }
}
