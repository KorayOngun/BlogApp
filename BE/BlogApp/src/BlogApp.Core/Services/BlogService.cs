using BlogApp.Core.Entities;
using BlogApp.Core.Repository;

namespace BlogApp.Core.Services;

public class BlogService : IBlogService
{
    private readonly IBlogRepository _blogRepository;
    public BlogService(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    private async Task EnsureUniqueTitle(Blog blog, CancellationToken cancellationToken)
    {
        if (await _blogRepository.TitleIsExist(blog.AuthorId, blog.Title))
            throw new Exception($"title is exist {blog.Title}");
    }

    public async Task AddAsync(Blog blog, CancellationToken cancellationToken)
    {
        await EnsureUniqueTitle(blog, cancellationToken);
        await _blogRepository.AddAsync(blog);
    }
}