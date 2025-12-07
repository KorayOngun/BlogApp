# TDD Ýçin Minimal Refactoring Rehberi

## ?? Adým 1: Custom Exception'lar Ekle

```csharp
// BlogApp.Application/Exceptions/BlogValidationException.cs
public class BlogValidationException : Exception
{
    public BlogValidationException(string message) : base(message) { }
}

// BlogApp.Application/Exceptions/DuplicateBlogTitleException.cs
public class DuplicateBlogTitleException : Exception
{
    public string Title { get; }
    public DuplicateBlogTitleException(string title) 
        : base($"Blog with title '{title}' already exists")
    {
        Title = title;
    }
}
```

## ?? Adým 2: Handler'ý Güncelle

```csharp
public async Task<CreateBlogResult> Handle(...)
{
    Blog blog = ToEntity(request);

    if (!_blogService.ValidateBlog(blog))
        throw new BlogValidationException("Blog validation failed"); // ?

    if(await _blogRepository.TitleIsExist(blog.AuthorId, blog.Title, cancellationToken))
        throw new DuplicateBlogTitleException(blog.Title); // ?

    await _blogRepository.AddAsync(blog);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new CreateBlogResult(blog.Id);
}
```

## ?? Adým 3: Test Güncelle

```csharp
[Test]
public void Handle_Should_Throw_BlogValidationException_When_Invalid()
{
    // Arrange
    _mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(false);

    // Act
    Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

    // Assert
    act.Should().ThrowAsync<BlogValidationException>() // ? Spesifik exception
        .WithMessage("Blog validation failed");
}

[Test]
public void Handle_Should_Throw_DuplicateBlogTitleException_With_Correct_Title()
{
    // Arrange
    var duplicateTitle = "Duplicate";
    _mockBlogRepository.TitleIsExist(...).Returns(true);

    // Act
    Func<Task> act = async () => await _handler.Handle(...);

    // Assert
    act.Should().ThrowAsync<DuplicateBlogTitleException>()
        .Where(ex => ex.Title == duplicateTitle); // ? Exception property testi
}
```

## ?? Ýleri Seviye (Opsiyonel)

### Mapper'ý Çýkar

```csharp
// 1. Interface oluþtur
public interface IBlogMapper
{
    Blog MapToEntity(CreateBlogCommand command, Guid authorId);
}

// 2. Implementation yaz
public class BlogMapper : IBlogMapper
{
    public Blog MapToEntity(CreateBlogCommand command, Guid authorId)
    {
        return new Blog
        {
            Title = command.Title,
            Content = command.Content,
            AuthorId = authorId
        };
    }
}

// 3. Handler'a inject et
public class CreateBlogCommandHandler(
    ...,
    IBlogMapper blogMapper) // ? Yeni dependency
{
    private readonly IBlogMapper _blogMapper = blogMapper;
    
    public async Task Handle(...)
    {
        var authorId = _userHandlerService.GetUserId();
        Blog blog = _blogMapper.MapToEntity(request, authorId); // ? Private metod yerine
        
        // ... rest of the code
    }
    
    // ? ToEntity() silinebilir
}

// 4. DI'a kaydet
services.AddScoped<IBlogMapper, BlogMapper>();
```

## ?? Test Avantajlarý

### Eski Yöntem (Private Method)
```csharp
[Test]
public async Task Should_Set_AuthorId()
{
    // 5 mock gerekli!
    _mockUserHandler.GetUserId().Returns(userId);
    _mockBlogService.ValidateBlog(...).Returns(true);
    _mockRepository.TitleIsExist(...).Returns(false);
    _mockRepository.AddAsync(...);
    _mockUnitOfWork.SaveChangesAsync(...).Returns(1);
    
    Blog? captured = null;
    await _mockRepository.AddAsync(Arg.Do<Blog>(b => captured = b));
    
    await _handler.Handle(command, CancellationToken.None);
    
    captured!.AuthorId.Should().Be(userId);
}
```

### Yeni Yöntem (Mapper Servisi)
```csharp
[Test]
public void MapToEntity_Should_Set_AuthorId()
{
    // 0 mock gerekli! ?
    var mapper = new BlogMapper();
    var blog = mapper.MapToEntity(command, expectedUserId);
    
    blog.AuthorId.Should().Be(expectedUserId); // Basit!
}
```

## ?? Özet

| Refactoring | Zorluk | Test Kazancý | Önerim |
|-------------|---------|--------------|--------|
| Custom Exceptions | ? Kolay | +2 test | ? Hemen yap |
| Mapper Extraction | ?? Orta | +4 test | ? Yap |
| Validator Extraction | ??? Zor | +6 test | ?? Gerekirse |
| Repository Abstraction | ???? Çok Zor | +10 test | ?? Ýleri seviye |

## ?? Hangi Deðiþikliði Yapmalýsýnýz?

1. **Acemi:** Custom exception'lar (5 dakika)
2. **Orta:** Mapper extraction (15 dakika)
3. **Ýleri:** Validator + Strategy pattern (1 saat)

Hangisini yapmak istersiniz?
