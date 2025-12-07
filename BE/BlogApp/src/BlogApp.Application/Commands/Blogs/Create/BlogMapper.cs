using BlogApp.Core.Entities;

namespace BlogApp.Application.Commands.Blogs.Create;

public class BlogMapper : IBlogMapper
{
    public Blog MapToEntity(CreateBlogCommand command, Guid authorId)
    {
        var date = DateTime.UtcNow;
        return new Blog
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Content = command.Content,
            AuthorId = authorId,
            CreatedAt = date,
            UpdatedAt = date
        };
    }
}
