using FluentAssertions;

namespace BlogApp.Core.UnitTests;

[TestFixture]
public class BaseEntityTests
{
    [Test]
    public void BaseEntity_Should_Have_Id_Property()
    {
        // Arrange & Act
        var entity = new BaseEntity();

        // Assert
        entity.Should().NotBeNull();
        entity.Id.Should().Be(Guid.Empty);
    }

    [Test]
    public void BaseEntity_Should_Allow_Setting_Id()
    {
        // Arrange
        var entity = new BaseEntity();
        var testId = Guid.NewGuid();

        // Act
        entity.Id = testId;

        // Assert
        entity.Id.Should().Be(testId);
        entity.Id.Should().NotBe(Guid.Empty);
    }

    [Test]
    public void BaseEntity_Should_Have_CreatedAt_Property()
    {
        // Arrange & Act
        var entity = new BaseEntity();
        var testDate = DateTime.UtcNow;
        entity.CreatedAt = testDate;

        // Assert
        entity.CreatedAt.Should().Be(testDate);
    }

    [Test]
    public void BaseEntity_Should_Have_UpdatedAt_Property()
    {
        // Arrange & Act
        var entity = new BaseEntity();
        var testDate = DateTime.UtcNow;
        entity.UpdatedAt = testDate;

        // Assert
        entity.UpdatedAt.Should().Be(testDate);
    }

    [Test]
    public void BaseEntity_CreatedAt_And_UpdatedAt_Should_Be_Independent()
    {
        // Arrange
        var entity = new BaseEntity();
        var createdDate = DateTime.UtcNow;
        var updatedDate = DateTime.UtcNow.AddHours(1);

        // Act
        entity.CreatedAt = createdDate;
        entity.UpdatedAt = updatedDate;

        // Assert
        entity.CreatedAt.Should().Be(createdDate);
        entity.UpdatedAt.Should().Be(updatedDate);
        entity.UpdatedAt.Should().BeAfter(entity.CreatedAt);
    }

    [Test]
    public void BaseEntity_Default_Values_Should_Be_Correct()
    {
        // Arrange & Act
        var entity = new BaseEntity();

        // Assert
        entity.Id.Should().Be(Guid.Empty);
        entity.CreatedAt.Should().Be(default(DateTime));
        entity.UpdatedAt.Should().Be(default(DateTime));
    }

    [Test]
    public void BaseEntity_Should_Be_Inheritable()
    {
        // Arrange
        var derivedType = typeof(BlogApp.Core.Entities.Blog);

        // Act & Assert
        derivedType.BaseType.Should().Be(typeof(BaseEntity));
    }
}
