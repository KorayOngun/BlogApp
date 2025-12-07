using BlogApp.Application.Commands.Blogs.Create;
using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using FluentAssertions;
using NSubstitute;

namespace BlogApp.Application.UnitTests.Commands.Blogs.Create;

[TestFixture]
public class CreateBlogCommandHandlerTests
{
    private IUserHandlerService _mockUserHandlerService;
    private IBlogService _mockBlogService;
    private IBlogRepository _mockBlogRepository;
    private IUnitOfWork _mockUnitOfWork;
    private IBlogMapper _mockBlogMapper;
    private CreateBlogCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        // Mock servisleri oluþtur
        _mockUserHandlerService = Substitute.For<IUserHandlerService>();
        _mockBlogService = Substitute.For<IBlogService>();
        _mockBlogRepository = Substitute.For<IBlogRepository>();
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _mockBlogMapper = Substitute.For<IBlogMapper>();

        // Handler'ý oluþtur
        _handler = new CreateBlogCommandHandler(
            _mockUserHandlerService,
            _mockBlogService,
            _mockBlogRepository,
            _mockUnitOfWork,
            _mockBlogMapper);
    }

    [Test]
    public async Task Handle_Should_Set_AuthorId_From_UserHandlerService()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "Test Blog Title",
            Content = "Test Blog Content"
        };

        var mappedBlog = new Blog
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Content = command.Content,
            AuthorId = expectedUserId
        };

        // Mock: IUserHandlerService.GetUserId() belirli bir Guid dönsün
        _mockUserHandlerService.GetUserId().Returns(expectedUserId);

        // Mock: Mapper returns blog with correct AuthorId
        _mockBlogMapper.MapToEntity(command, expectedUserId).Returns(mappedBlog);

        // Mock: BlogService validation baþarýlý
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);

        // Mock: Title unique (mevcut deðil)
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);

        // Mock: UnitOfWork baþarýlý
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedBlog.Should().NotBeNull("Blog repository'ye eklenmeli");
        capturedBlog!.AuthorId.Should().Be(expectedUserId, "UserHandlerService'den gelen UserId set edilmeli");
        capturedBlog.Title.Should().Be(command.Title);
        capturedBlog.Content.Should().Be(command.Content);

        // IUserHandlerService.GetUserId() çaðrýldý mý?
        _mockUserHandlerService.Received(1).GetUserId();
        
        // Mapper doðru parametrelerle çaðrýldý mý?
        _mockBlogMapper.Received(1).MapToEntity(command, expectedUserId);
    }

    [Test]
    public async Task Handle_Should_Call_UserHandlerService_Once()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "Test Title",
            Content = "Test Content"
        };

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockUserHandlerService.Received(1).GetUserId();
    }

    [Test]
    public async Task Handle_Should_Not_Set_Empty_AuthorId()
    {
        // Arrange
        var validUserId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "Test Title",
            Content = "Test Content"
        };

        _mockUserHandlerService.GetUserId().Returns(validUserId);
        _mockBlogMapper.MapToEntity(command, validUserId).Returns(new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = validUserId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedBlog.Should().NotBeNull();
        capturedBlog!.AuthorId.Should().NotBe(Guid.Empty, "AuthorId boþ olmamalý");
        capturedBlog.AuthorId.Should().Be(validUserId);
    }

    [Test]
    public async Task Handle_Should_Pass_Correct_AuthorId_To_TitleIsExist()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "Unique Title",
            Content = "Content"
        };

        _mockUserHandlerService.GetUserId().Returns(expectedUserId);
        _mockBlogMapper.MapToEntity(command, expectedUserId).Returns(new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = expectedUserId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        // TitleIsExist çaðrýsýnda AuthorId doðru gönderildi mi?
        await _mockBlogRepository.Received(1).TitleIsExist(
            expectedUserId, // UserHandlerService'den gelen ID
            command.Title,
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_Should_Create_Valid_Blog_Entity()
    {
        // Arrange
        var userId = Guid.Parse("12345678-1234-1234-1234-123456789012");
        var command = new CreateBlogCommand
        {
            Title = "My Blog Post",
            Content = "This is the content of my blog."
        };

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedBlog.Should().NotBeNull();
        capturedBlog!.Title.Should().Be("My Blog Post");
        capturedBlog.Content.Should().Be("This is the content of my blog.");
        capturedBlog.AuthorId.Should().Be(userId);
        capturedBlog.Should().BeOfType<Blog>();
    }

    [Test]
    public void Handle_Should_Throw_Exception_When_Validation_Fails()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "",
            Content = "Content"
        };

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(false); // Validation FAILED

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<Exception>()
            .WithMessage("Blog validation failed");
    }

    [Test]
    public void Handle_Should_Throw_Exception_When_Title_Already_Exists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "Duplicate Title",
            Content = "Content"
        };

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(true); // Title zaten mevcut

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        act.Should().ThrowAsync<Exception>()
            .WithMessage("Blog with title 'Duplicate Title' already exists");
    }

    [Test]
    public async Task Handle_Should_Return_Blog_Id_In_Result()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var blogId = Guid.NewGuid();
        var command = new CreateBlogCommand
        {
            Title = "Test",
            Content = "Content"
        };

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Id = blogId,
            Title = command.Title,
            Content = command.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(blogId, "Result'taki Id, mapper tarafýndan set edilen Id ile ayný olmalý");
        result.Id.Should().Be(capturedBlog!.Id, "Result'taki Id, oluþturulan blog'un Id'si ile ayný olmalý");
    }
}
