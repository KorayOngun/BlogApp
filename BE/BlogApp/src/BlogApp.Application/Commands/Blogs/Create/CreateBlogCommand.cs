using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommand : IRequest<CreateBlogResult>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
}
