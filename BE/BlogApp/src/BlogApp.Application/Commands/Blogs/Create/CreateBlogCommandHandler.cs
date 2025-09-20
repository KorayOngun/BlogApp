using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, CreateBlogResult>
{
    private readonly IBlogRepository _blogRepository;
    private readonly IBlogService _blogService;
    private readonly IUserHandlerService _userHandlerService;
    public CreateBlogCommandHandler(IBlogRepository blogRepository, IUserHandlerService userHandlerService, IBlogService blogService)
    {
        _blogRepository = blogRepository;
        _userHandlerService = userHandlerService;
        _blogService = blogService;
    }
    public async Task<CreateBlogResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        Blog blog = ToEntity(request);

        await _blogService.EnsureUniqueTitle(blog, cancellationToken);

        await _blogRepository.AddAsync(blog);
        await _blogRepository.SaveChangesAsync();
        return new CreateBlogResult(blog.Id);
    }

    private Blog ToEntity(CreateBlogCommand request)
    {
        return new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = _userHandlerService.GetUserId(),
        };
    }
}
