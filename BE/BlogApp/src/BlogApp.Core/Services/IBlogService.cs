using BlogApp.Core.Entities;

namespace BlogApp.Core.Services;

public interface IBlogService
{
  Task EnsureUniqueTitle(Blog blog, CancellationToken cancellationToken);
}
