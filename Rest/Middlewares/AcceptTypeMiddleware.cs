using Microsoft.Net.Http.Headers;

namespace MyCleanArchitecture.Middlewares
{
    public class AcceptTypeMiddleware : IMiddleware
    {
        private readonly string[] RIGHT_ACCEPTS = { "*/*", "*/json", "application/*", "application/json" };
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var acceptHeader = context.Request.Headers[HeaderNames.Accept];

            if (!acceptHeader.Any(v=> RIGHT_ACCEPTS.Intersect(v.Split(',', ';')).Any()))
            {
                context.Response.StatusCode = StatusCodes.Status406NotAcceptable;
                await context.Response.WriteAsync("Not Acceptable");
                return;
            }

            await next(context);


        }
    }
}
