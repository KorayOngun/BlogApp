using Microsoft.EntityFrameworkCore;

namespace BlogApp.Persistence.Contexts;

public class BlogAppContext(DbContextOptions dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<BlogApp.Core.Entities.Blog> Blogs { get; set; }
}

