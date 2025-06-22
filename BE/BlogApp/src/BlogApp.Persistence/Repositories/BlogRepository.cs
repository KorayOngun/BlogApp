using BlogApp.Core.Repository;
using BlogApp.Persistence.Contexts;

namespace BlogApp.Persistence.Repositories;

public class BlogRepository(BlogAppContext blogAppContext) : IBlogRepository
{
    private readonly BlogAppContext _blogAppContext = blogAppContext;

    public async Task AddAsync(BlogApp.Core.Entities.Blog blog)
    {
        await _blogAppContext.Blogs.AddAsync(blog);
        await _blogAppContext.SaveChangesAsync();
    }

    public async Task<BlogApp.Core.Entities.Blog?> GetByIdAsync(Guid id)
    {
        return await _blogAppContext.Blogs.FindAsync(id);
    }
}
