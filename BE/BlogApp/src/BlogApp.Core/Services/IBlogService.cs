using BlogApp.Core.Entities;

namespace BlogApp.Core.Services;

public interface IBlogService
{
    Task AddAsync(Blog blog, CancellationToken cancellationToken);
}
