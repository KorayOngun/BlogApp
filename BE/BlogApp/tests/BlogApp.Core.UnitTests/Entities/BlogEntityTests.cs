using BlogApp.Core.Entities;
using FluentAssertions;

namespace BlogApp.Core.UnitTests.Entities;

[TestFixture]
public class BlogEntityTests
{
    [Test]
    public void Blog_Creation_Should_Set_Properties_Correctly()
    {
        // Arrange
        var authorId = Guid.NewGuid();
        var title = "Test Blog Title";
        var content = "This is a test blog content.";

        // Act
        var blog = new Blog
        {
            Title = title,
            Content = content,
            AuthorId = authorId
        };

        // Assert
        blog.Title.Should().Be(title);
        blog.Content.Should().Be(content);
        blog.AuthorId.Should().Be(authorId);
    }

    [Test]
    public void Blog_Should_Inherit_From_BaseEntity()
    {
        // Arrange & Act
        var blog = new Blog
        {
            Title = "Test",
            Content = "Content",
            AuthorId = Guid.NewGuid()
        };

        // Assert
        blog.Should().BeAssignableTo<BaseEntity>();
        blog.Id.Should().Be(Guid.Empty); // Default deðer
    }

    [Test]
    public void Blog_Should_Have_BaseEntity_Properties()
    {
        // Arrange
        var blog = new Blog
        {
            Title = "Test",
            Content = "Content",
            AuthorId = Guid.NewGuid()
        };
        var testId = Guid.NewGuid();
        var createdAt = DateTime.UtcNow;
        var updatedAt = DateTime.UtcNow.AddHours(1);

        // Act
        blog.Id = testId;
        blog.CreatedAt = createdAt;
        blog.UpdatedAt = updatedAt;

        // Assert
        blog.Id.Should().Be(testId);
        blog.CreatedAt.Should().Be(createdAt);
        blog.UpdatedAt.Should().Be(updatedAt);
    }

    [Test]
    public void Blog_AuthorId_Should_Not_Be_Empty()
    {
        // Arrange
        var authorId = Guid.NewGuid();

        // Act
        var blog = new Blog
        {
            Title = "Test",
            Content = "Content",
            AuthorId = authorId
        };

        // Assert
        blog.AuthorId.Should().NotBe(Guid.Empty);
        blog.AuthorId.Should().Be(authorId);
    }

    [TestCase("")]
    [TestCase("Short Title")]
    [TestCase("A Very Long Blog Title That Describes Everything In Detail")]
    public void Blog_Title_Should_Accept_Various_Lengths(string title)
    {
        // Arrange & Act
        var blog = new Blog
        {
            Title = title,
            Content = "Content",
            AuthorId = Guid.NewGuid()
        };

        // Assert
        blog.Title.Should().Be(title);
    }

    [Test]
    public void Blog_Content_Can_Be_Long_Text()
    {
        // Arrange
        var longContent = string.Join(" ", Enumerable.Repeat("Lorem ipsum", 1000));

        // Act
        var blog = new Blog
        {
            Title = "Test",
            Content = longContent,
            AuthorId = Guid.NewGuid()
        };

        // Assert
        blog.Content.Should().Be(longContent);
        blog.Content.Length.Should().BeGreaterThan(5000);
    }
}
