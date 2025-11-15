using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler(IBlogRepository blogRepository, IUserHandlerService userHandlerService, IBlogService blogService, IUnitOfWork unitOfWork) : IRequestHandler<CreateBlogCommand, CreateBlogResult>
{
    private readonly IBlogRepository _blogRepository = blogRepository;
    private readonly IBlogService _blogService = blogService;
    private readonly IUserHandlerService _userHandlerService = userHandlerService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateBlogResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        Blog blog = ToEntity(request);
        await _blogService.AddAsync(blog, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
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
