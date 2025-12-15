using BlogApp.Application.Mappers;
using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using BlogApp.Core.ValueObjects;
using BlogApp.MessageContracts.Responses.Blogs;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler(
    IUserHandlerService userHandlerService,
    IBlogService blogService,
    IBlogRepository blogRepository,
    IUnitOfWork unitOfWork,
    IBlogMapper blogMapper) : IRequestHandler<CreateBlogCommand, Result<CreateBlogResponse>>

{
    private readonly IBlogService _blogService = blogService;
    private readonly IUserHandlerService _userHandlerService = userHandlerService;
    private readonly IBlogRepository _blogRepository = blogRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IBlogMapper _blogMapper = blogMapper;

    public async Task<Result<CreateBlogResponse>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var authorId = _userHandlerService.GetUserId();
        Blog blog = _blogMapper.MapToEntity(request, authorId);

        var validateResult = _blogService.ValidateBlog(blog);
        if (validateResult.IsError)
            return validateResult.AsError();

        var titleControl = await _blogService.IsTitleExistForAuthorAsync(blog.AuthorId, blog.Title, cancellationToken);
        if (titleControl)
            return Result.Error("A blog with this title already exists for the author.");

        await _blogRepository.AddAsync(blog);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateBlogResponse(blog.Id);
    }
}
