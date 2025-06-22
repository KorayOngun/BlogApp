using BlogApp.Core.Entities;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Guid>
{
    private readonly Core.Repository.IBlogRepository _blogRepository;
    public CreateBlogCommandHandler(Core.Repository.IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }
    public async Task<Guid> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var blog = new Blog()
        {
            AuthorId = request.AuthorId,
            Content = request.Content,
            Title = request.Title,
        };
        await _blogRepository.AddAsync(blog);
        return blog.Id;
    }
}
