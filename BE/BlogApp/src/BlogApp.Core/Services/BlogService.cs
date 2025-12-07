using BlogApp.Core.Entities;

namespace BlogApp.Core.Services;

public class BlogService : IBlogService
{
    public bool ValidateBlog(Blog blog)
    {
        if (blog.AuthorId == Guid.Empty)
            return false;

        if(string.IsNullOrWhiteSpace(blog.Title))
            return false;

        return true;
    }
}