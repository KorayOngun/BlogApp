using BlogApp.Application.Commands.Blogs.Create;
using BlogApp.Application.Mappers;
using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using BlogApp.Core.ValueObjects;
using BlogApp.MessageContracts.Requests.Blogs;
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
        // Mock servisleri olu�tur
        _mockUserHandlerService = Substitute.For<IUserHandlerService>();
        _mockBlogService = Substitute.For<IBlogService>();
        _mockBlogRepository = Substitute.For<IBlogRepository>();
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _mockBlogMapper = Substitute.For<IBlogMapper>();

        // Handler'� olu�tur
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
        var request = new CreateBlogRequest
        {
            Title = "Test Blog Title",
            Content = "Test Blog Content"
        };
        var command = new CreateBlogCommand(request);

        var mappedBlog = new Blog
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Content = request.Content,
            AuthorId = expectedUserId
        };

        // Mock: IUserHandlerService.GetUserId() belirli bir Guid d�ns�n
        _mockUserHandlerService.GetUserId().Returns(expectedUserId);

        // Mock: Mapper returns blog with correct AuthorId
        _mockBlogMapper.MapToEntity(command, expectedUserId).Returns(mappedBlog);

        // Mock: BlogService validation ba�ar�l�
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());

        // Mock: Title unique (mevcut de�il)
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);

        // Mock: UnitOfWork ba�ar�l�
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedBlog.Should().NotBeNull("Blog repository'ye eklenmeli");
        capturedBlog!.AuthorId.Should().Be(expectedUserId, "UserHandlerService'den gelen UserId set edilmeli");
        capturedBlog.Title.Should().Be(request.Title);
        capturedBlog.Content.Should().Be(request.Content);

        // IUserHandlerService.GetUserId() �a�r�ld� m�?
        _mockUserHandlerService.Received(1).GetUserId();

        // Mapper do�ru parametrelerle �a�r�ld� m�?
        _mockBlogMapper.Received(1).MapToEntity(command, expectedUserId);
    }

    [Test]
    public async Task Handle_Should_Call_UserHandlerService_Once()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var request = new CreateBlogRequest
        {
            Title = "Test Title",
            Content = "Test Content"
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());
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
        var request = new CreateBlogRequest
        {
            Title = "Test Title",
            Content = "Test Content"
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(validUserId);
        _mockBlogMapper.MapToEntity(command, validUserId).Returns(new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = validUserId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        capturedBlog.Should().NotBeNull();
        capturedBlog!.AuthorId.Should().NotBe(Guid.Empty, "AuthorId bo� olmamal�");
        capturedBlog.AuthorId.Should().Be(validUserId);
    }

    [Test]
    public async Task Handle_Should_Pass_Correct_AuthorId_To_TitleIsExist()
    {
        // Arrange
        var expectedUserId = Guid.NewGuid();
        var request = new CreateBlogRequest
        {
            Title = "Unique Title",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(expectedUserId);
        _mockBlogMapper.MapToEntity(command, expectedUserId).Returns(new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = expectedUserId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());
        _mockBlogService.IsTitleExistForAuthorAsync(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        // IsTitleExistForAuthorAsync �a�r�s�nda AuthorId do�ru g�nderildi mi?
        await _mockBlogService.Received(1).IsTitleExistForAuthorAsync(
            expectedUserId, // UserHandlerService'den gelen ID
            request.Title,
            Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_Should_Create_Valid_Blog_Entity()
    {
        // Arrange
        var userId = Guid.Parse("12345678-1234-1234-1234-123456789012");
        var request = new CreateBlogRequest
        {
            Title = "My Blog Post",
            Content = "This is the content of my blog."
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());
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
        var request = new CreateBlogRequest
        {
            Title = "",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Error()); // Validation FAILED

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
        var request = new CreateBlogRequest
        {
            Title = "Duplicate Title",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());
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
        var request = new CreateBlogRequest
        {
            Title = "Test",
            Content = "Content"
        };
        var command = new CreateBlogCommand(request);

        _mockUserHandlerService.GetUserId().Returns(userId);
        _mockBlogMapper.MapToEntity(command, userId).Returns(new Blog
        {
            Id = blogId,
            Title = request.Title,
            Content = request.Content,
            AuthorId = userId
        });
        _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(Result.Ok());
        _mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(false);
        _mockUnitOfWork.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        Blog? capturedBlog = null;
        await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Value.Should().NotBeNull();
        result.Value?.Id.Should().Be(blogId, "Result'taki Id, mapper taraf�ndan set edilen Id ile ayn� olmal�");
        result.Value?.Id.Should().Be(capturedBlog!.Id, "Result'taki Id, olu�turulan blog'un Id'si ile ayn� olmal�");
    }
}
