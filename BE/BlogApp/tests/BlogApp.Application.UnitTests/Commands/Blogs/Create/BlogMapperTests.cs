using BlogApp.Application.Commands.Blogs.Create;
using BlogApp.Application.Mappers;
using BlogApp.Core.Entities;
using BlogApp.MessageContracts.Requests.Blogs;
using FluentAssertions;

namespace BlogApp.Application.UnitTests.Commands.Blogs.Create;


[TestFixture]
public class BlogMapperTests
{
    private BlogMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new BlogMapper();
    }

    [Test]
    public void MapToEntity_Should_Set_All_Properties_Correctly()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var request = new CreateBlogRequest
        {
            Title = "Test Blog Title",
            Content = "Test Blog Content"
        };
        var command = new CreateBlogCommand(request);

        // Act
        var result = _mapper.MapToEntity(command, authorId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBe(Guid.Empty, "Id should be generated");
        result.Title.Should().Be(request.Title);
        result.Content.Should().Be(request.Content);
        result.AuthorId.Should().Be(authorId);
        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Test]
    public void MapToEntity_Should_Generate_Unique_Ids()
    {
        // Arrange
        var request = new CreateBlogRequest
        {
            Title = "Test",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        // Act
        var blog1 = _mapper.MapToEntity(command, Guid.NewGuid());
        var blog2 = _mapper.MapToEntity(command, Guid.NewGuid());

        // Assert
        blog1.Id.Should().NotBe(blog2.Id, "Each blog should have a unique ID");
    }

    [Test]
    public void MapToEntity_Should_Set_CreatedAt_And_UpdatedAt_To_Same_Time()
    {
        // Arrange
        var request = new CreateBlogRequest
        {
            Title = "Test",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        // Act
        var result = _mapper.MapToEntity(command, Guid.NewGuid());

        // Assert
        result.CreatedAt.Should().Be(result.UpdatedAt, "CreatedAt and UpdatedAt should be the same on creation");
    }

    [TestCase("")]
    [TestCase("Short")]
    [TestCase("A very long title that might exceed normal length constraints for testing purposes")]
    public void MapToEntity_Should_Handle_Various_Title_Lengths(string title)
    {
        // Arrange
        var request = new CreateBlogRequest
        {
            Title = title,
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        // Act
        var result = _mapper.MapToEntity(command, Guid.NewGuid());

        // Assert
        result.Title.Should().Be(title);
    }

    [Test]
    public void MapToEntity_Should_Set_AuthorId_From_Parameter()
    {
        // Arrange
        var expectedAuthorId = Guid.Parse("12345678-1234-1234-1234-123456789012");
        var request = new CreateBlogRequest
        {
            Title = "Test",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        // Act
        var result = _mapper.MapToEntity(command, expectedAuthorId);

        // Assert
        result.AuthorId.Should().Be(expectedAuthorId, "AuthorId should be set from parameter, not generated");
    }

    [Test]
    public void MapToEntity_Should_Return_Blog_Type()
    {
        // Arrange
        var request = new CreateBlogRequest
        {
            Title = "Test",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        // Act
        var result = _mapper.MapToEntity(command, Guid.NewGuid());

        // Assert
        result.Should().BeOfType<Blog>();
    }
}
