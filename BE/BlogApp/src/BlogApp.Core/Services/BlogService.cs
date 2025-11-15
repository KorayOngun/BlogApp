using BlogApp.Core.Entities;
using BlogApp.Core.Repository;

namespace BlogApp.Core.Services;

public class BlogService(IBlogRepository blogRepository) : IBlogService
{
    private readonly IBlogRepository _blogRepository = blogRepository;

    private async Task<bool> EnsureUniqueTitle(Blog blog, CancellationToken cancellationToken)
    {
        if (await _blogRepository.TitleIsExist(blog.AuthorId, blog.Title, cancellationToken))
            return false;
        return true;
    }

    public async Task AddAsync(Blog blog, CancellationToken cancellationToken)
    {
        var titleControl = await EnsureUniqueTitle(blog, cancellationToken);
        if (!titleControl)
            throw new Exception();
        await _blogRepository.AddAsync(blog);
    }
}