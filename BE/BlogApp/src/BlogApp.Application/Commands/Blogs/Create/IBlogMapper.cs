using BlogApp.Core.Entities;

namespace BlogApp.Application.Commands.Blogs.Create;

public interface IBlogMapper
{
    Blog MapToEntity(CreateBlogCommand command, Guid authorId);
}
