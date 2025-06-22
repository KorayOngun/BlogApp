using BlogApp.Core.Entities;

namespace BlogApp.Core.Repository;

public interface IBlogRepository
{
    Task AddAsync(Blog blog);
    Task<Blog?> GetByIdAsync(Guid id);
}
