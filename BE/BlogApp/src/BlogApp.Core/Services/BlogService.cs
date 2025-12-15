using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.ValueObjects;

namespace BlogApp.Core.Services;

public class BlogService(IBlogRepository blogRepository) : IBlogService
{
    private readonly IBlogRepository _blogRepository = blogRepository;

    public Result ValidateBlog(Blog blog)
    {
        if (blog.AuthorId == Guid.Empty)
            return Result.Error("user id null olamaz");

        if (string.IsNullOrWhiteSpace(blog.Title))
            return Result.Error("title bos olamaz");

        return Result.Ok();
    }

    public async Task<bool> IsTitleExistForAuthorAsync(Guid authorId, string title, CancellationToken cancellationToken)
    {
        return await _blogRepository.TitleIsExist(authorId, title, cancellationToken);
    }
}