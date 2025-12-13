using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using FluentAssertions;
using NSubstitute;

namespace BlogApp.Core.UnitTests.Services;

[TestFixture]
public class BlogServiceTests
{
    private BlogService _blogService;
    private IBlogRepository _blogRepository;

    [SetUp]
    public void Setup()
    {
        _blogRepository = Substitute.For<IBlogRepository>();
        _blogService = new BlogService(_blogRepository);
    }

    [Test]
    public void ValidateBlog_WithValidBlog_Should_Return_True()
    {
        // Arrange
        var validBlog = new Blog
        {
            Title = "Valid Blog Title",
            Content = "Valid blog content with sufficient details.",
            AuthorId = Guid.NewGuid()
        };

        // Act
        var result = _blogService.ValidateBlog(validBlog);

        // Assert
        result.IsOk.Should().BeTrue();
    }

    [Test]
    public void ValidateBlog_WithEmptyAuthorId_Should_Return_False()
    {
        // Arrange
        var blogWithEmptyAuthor = new Blog
        {
            Title = "Valid Title",
            Content = "Valid Content",
            AuthorId = Guid.Empty
        };

        // Act
        var result = _blogService.ValidateBlog(blogWithEmptyAuthor);

        // Assert
        result.IsOk.Should().BeFalse("AuthorId cannot be empty");
    }

    [TestCase("")]
    [TestCase("   ")]
    public void ValidateBlog_WithInvalidTitle_Should_Return_False(string invalidTitle)
    {
        // Arrange
        var blogWithInvalidTitle = new Blog
        {
            Title = invalidTitle,
            Content = "Valid Content",
            AuthorId = Guid.NewGuid()
        };

        // Act
        var result = _blogService.ValidateBlog(blogWithInvalidTitle);

        // Assert
        result.IsOk.Should().BeFalse("Title cannot be null, empty or whitespace");
    }

    [Test]
    public void ValidateBlog_WithNullTitle_Should_Return_False()
    {
        // Arrange
        var blogWithNullTitle = new Blog
        {
            Title = null!,
            Content = "Valid Content",
            AuthorId = Guid.NewGuid()
        };

        // Act
        var result = _blogService.ValidateBlog(blogWithNullTitle);

        // Assert
        result.IsOk.Should().BeFalse("Title cannot be null");
    }

    [Test]
    public void ValidateBlog_WithEmptyAuthorIdAndEmptyTitle_Should_Return_False()
    {
        // Arrange
        var invalidBlog = new Blog
        {
            Title = "",
            Content = "Valid Content",
            AuthorId = Guid.Empty
        };

        // Act
        var result = _blogService.ValidateBlog(invalidBlog);

        // Assert
        result.IsOk.Should().BeFalse("Both AuthorId and Title are invalid");
    }

    [Test]
    public void ValidateBlog_WithAllValidFields_Should_Return_True()
    {
        // Arrange
        var blog = new Blog
        {
            Title = "Comprehensive Blog Title",
            Content = "This is a comprehensive blog content with all necessary information.",
            AuthorId = Guid.Parse("12345678-1234-1234-1234-123456789012")
        };

        // Act
        var result = _blogService.ValidateBlog(blog);

        // Assert
        result.IsOk.Should().BeTrue();
        blog.AuthorId.Should().NotBe(Guid.Empty);
        blog.Title.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void ValidateBlog_EmptyContent_Should_Still_Pass_Validation()
    {
        // Arrange
        // Content validation yoksa geçmeli
        var blog = new Blog
        {
            Title = "Valid Title",
            Content = "",
            AuthorId = Guid.NewGuid()
        };

        // Act
        var result = _blogService.ValidateBlog(blog);

        // Assert
        result.IsOk.Should().BeTrue("Content validation is not implemented in service");
    }

    [Test]
    public async Task IsTitleExistForAuthorAsync_WhenTitleExists_Should_Return_True()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var title = "Existing Blog Title";
        _blogRepository.TitleIsExist(authorId, title, Arg.Any<CancellationToken>())
            .Returns(true);

        // Act
        var result = await _blogService.IsTitleExistForAuthorAsync(authorId, title, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
        await _blogRepository.Received(1).TitleIsExist(authorId, title, Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task IsTitleExistForAuthorAsync_WhenTitleDoesNotExist_Should_Return_False()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var title = "New Blog Title";
        _blogRepository.TitleIsExist(authorId, title, Arg.Any<CancellationToken>())
            .Returns(false);

        // Act
        var result = await _blogService.IsTitleExistForAuthorAsync(authorId, title, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
        await _blogRepository.Received(1).TitleIsExist(authorId, title, Arg.Any<CancellationToken>());
    }
}
