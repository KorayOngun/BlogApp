using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler(IBlogRepository blogRepository) : IRequestHandler<CreateBlogCommand, CreateBlogResult>
{
    private readonly IBlogRepository _blogRepository = blogRepository;

    public async Task<CreateBlogResult> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        Blog blog = ToEntity(request);
        if (await _blogRepository.UniqueTitleControl(blog.AuthorId, blog.Title))
            throw new Exception();

        await _blogRepository.AddAsync(blog);
        await _blogRepository.SaveChangesAsync();
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
