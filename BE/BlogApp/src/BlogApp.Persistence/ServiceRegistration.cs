using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<Contexts.BlogAppContext>(options =>
        {
            options.UseInMemoryDatabase("BlogApp");
        });

        services.AddScoped<Core.Repository.IBlogRepository, Repositories.BlogRepository>();
    }
}
