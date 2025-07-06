using BlogApp.Application.Services;
using BlogApp.Core.Entities;
using BlogApp.Core.Repository;

namespace BlogApp.Infrastructure.Services;

public class BlogService(
        IBlogRepository blogRepository
    ) : IBlogService
{
    private readonly IBlogRepository _blogRepository = blogRepository;

    public async Task<Guid> CreateBlogAsync(Blog blog)
    {
        await _blogRepository.AddAsync(blog);
        await _blogRepository.SaveChangesAsync();
        return blog.Id;
    }
}
