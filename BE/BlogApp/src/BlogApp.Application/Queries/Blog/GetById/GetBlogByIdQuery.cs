using MediatR;

namespace BlogApp.Application.Queries.Blog.GetById;

public class GetBlogByIdQuery : IRequest<BlogApp.Core.Entities.Blog?>
{
    public Guid Id { get; set; }
}
