using BlogApp.Application.Mappers;
using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using BlogApp.MessageContracts.Responses.Blogs;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler(
    IUserHandlerService userHandlerService,
    IBlogService blogService,
    IBlogRepository blogRepository,
    IUnitOfWork unitOfWork,
    IBlogMapper blogMapper) : IRequestHandler<CreateBlogCommand, CreateBlogResponse>
{
    private readonly IBlogService _blogService = blogService;
    private readonly IUserHandlerService _userHandlerService = userHandlerService;
    private readonly IBlogRepository _blogRepository = blogRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBlogMapper _blogMapper = blogMapper;

    public async Task<CreateBlogResponse> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var authorId = _userHandlerService.GetUserId();
        Blog blog = _blogMapper.MapToEntity(request, authorId);

        if (!_blogService.ValidateBlog(blog))
            throw new Exception("Blog validation failed");

        if(await _blogRepository.TitleIsExist(blog.AuthorId, blog.Title, cancellationToken))
            throw new Exception($"Blog with title '{blog.Title}' already exists");

        await _blogRepository.AddAsync(blog);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBlogResponse(blog.Id);
    }
}
