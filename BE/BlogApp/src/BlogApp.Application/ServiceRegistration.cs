using BlogApp.Application.Commands.Blogs.Create;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(ServiceRegistration)));
        
        // Mappers
        services.AddScoped<IBlogMapper, BlogMapper>();
    }
}
