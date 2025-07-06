using BlogApp.Application.Services;
using BlogApp.Core.Entities;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler(IBlogService blogService) : IRequestHandler<CreateBlogCommand, CreateBlogResult>
{
    private readonly IBlogService _blogService = blogService;

    public async Task<CreateBlogResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {

        Blog blog = ToEntity(request);
        await _blogService.CreateBlogAsync(blog);
        return new CreateBlogResult(blog.Id);
    }

    private static Blog ToEntity(CreateBlogCommand request)
    {
        return new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = Guid.NewGuid(),
        };
    }
}
