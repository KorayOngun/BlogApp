using BlogApp.Core.Repository;
using MediatR;

namespace BlogApp.Application.Queries.Blog.GetById;

public class GetBlogByIdQueryHandler(IBlogRepository blogRepository) : IRequestHandler<GetBlogByIdQuery, BlogApp.Core.Entities.Blog?>
{
    private readonly IBlogRepository _blogRepository = blogRepository;

    public async Task<BlogApp.Core.Entities.Blog?> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await _blogRepository.GetByIdAsync(request.Id);
        return blog;
    }
}