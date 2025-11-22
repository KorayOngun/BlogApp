using BlogApp.Core.Entities;

namespace BlogApp.Core.Services;

public interface IBlogService
{
    bool ValidateBlog(Blog blog);
}
