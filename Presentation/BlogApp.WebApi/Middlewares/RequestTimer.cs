
using System.Diagnostics;

namespace BlogApp.WebApi.Middlewares
{
    public class RequestTimer : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await next(context);
            stopwatch.Stop();
            Console.WriteLine(stopwatch);
        }
    }
}
