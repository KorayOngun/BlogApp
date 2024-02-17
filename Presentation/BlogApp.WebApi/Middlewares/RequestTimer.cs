
namespace BlogApp.WebApi.Middlewares
{
    public class RequestTimer : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           
            var tic = DateTime.UtcNow;
            await next(context);
            var toc = DateTime.UtcNow;
            Console.WriteLine(toc-tic); 
        }
    }
}
