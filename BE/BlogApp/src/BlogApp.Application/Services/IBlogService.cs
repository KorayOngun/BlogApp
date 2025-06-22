using BlogApp.Core.Entities;

namespace BlogApp.Application.Services;

public interface IBlogService
{
    Task<Guid> CreateBlogAsync(Blog blog);
}
