using BlogApp.Core.Entities;
using BlogApp.Core.ValueObjects;

namespace BlogApp.Core.Services;

public interface IBlogService
{
    Result ValidateBlog(Blog blog);
    Task<bool> IsTitleExistForAuthorAsync(Guid authorId, string title, CancellationToken cancellationToken);
}
