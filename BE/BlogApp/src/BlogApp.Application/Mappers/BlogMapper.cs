using BlogApp.Application.Commands.Blogs.Create;
using BlogApp.Core.Entities;

namespace BlogApp.Application.Mappers;

public interface IBlogMapper
{
    Blog MapToEntity(CreateBlogCommand command, Guid authorId);
}


public class BlogMapper : IBlogMapper
{
    public Blog MapToEntity(CreateBlogCommand command, Guid authorId)
    {
        var date = DateTime.UtcNow;
        return new Blog
        {
            Id = Guid.NewGuid(),
            Title = command.Request.Title,
            Content = command.Request.Content,
            AuthorId = authorId,
            CreatedAt = date,
            UpdatedAt = date
        };
    }
}
