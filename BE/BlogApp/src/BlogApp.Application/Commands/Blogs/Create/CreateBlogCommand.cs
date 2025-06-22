using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommand : IRequest<Guid>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid AuthorId { get; set; }
}
