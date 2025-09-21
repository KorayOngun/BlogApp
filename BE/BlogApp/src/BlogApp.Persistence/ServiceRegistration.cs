using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
    services.AddScoped<Core.Repository.IBlogRepository, Repositories.BlogRepository>();
    services.AddScoped<Core.Repository.IUnitOfWork, Repositories.UnitOfWork>();
    }
    public static void AddBlogAppDbContext(this IServiceCollection services, BlogAppDbContextOptions? blogAppOptions = null)
    {
        services.AddDbContext<Contexts.BlogAppContext>(options =>
        {
            options.UseInMemoryDatabase("BlogApp");
        });
    }
}

public record BlogAppDbContextOptions()
{
    public DatabaseProvider dbProvider = DatabaseProvider.InMemoryDatabase;
}
public enum DatabaseProvider
{
    InMemoryDatabase,
    PSQL,
    MSSQL
}