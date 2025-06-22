using BlogApp.Application.Services;
using BlogApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IBlogService, BlogService>();
    }
}
