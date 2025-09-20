using BlogApp.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Core;

public static class ServiceRegistration
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IBlogService, BlogService>();
    }
}