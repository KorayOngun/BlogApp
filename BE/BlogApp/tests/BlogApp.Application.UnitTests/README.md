# BlogApp.Application.UnitTests

NUnit ve NSubstitute kullanýlarak yazýlmýþ BlogApp.Application katmaný için MediatR handler testleri.

## ?? Test Kapsamý

- ? **8 Test** - Tümü baþarýlý
- ? **1.1s** - Test süresi
- ?? **CreateBlogCommandHandler** - Tam test coverage

## ?? Test Senaryolarý

### ? Ana Test: `Handle_Should_Set_AuthorId_From_UserHandlerService`

**Sorunuz:** `IUserHandlerService.GetUserId()` metodundan gelen UserId'nin Blog nesnesine doðru set edildiðini nasýl test ederim?

**Cevap:** NSubstitute ile mock'layarak ve `Arg.Do<>` ile yakalayarak:

```csharp
[Test]
public async Task Handle_Should_Set_AuthorId_From_UserHandlerService()
{
    // Arrange
    var expectedUserId = Guid.NewGuid();
    
    // Mock: IUserHandlerService.GetUserId() belirli bir Guid dönsün
    _mockUserHandlerService.GetUserId().Returns(expectedUserId);
    
    // Blog nesnesini yakala
    Blog? capturedBlog = null;
    await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));
    
    // Act
    await _handler.Handle(command, CancellationToken.None);
    
    // Assert
    capturedBlog!.AuthorId.Should().Be(expectedUserId, 
        "UserHandlerService'den gelen UserId set edilmeli");
}
```

## ?? Test Kategorileri

### 1. **UserId Set Kontrolü** (3 Test)
- ? `Handle_Should_Set_AuthorId_From_UserHandlerService`
  - UserHandlerService'den dönen ID'nin Blog'a atandýðýný doðrular
- ? `Handle_Should_Call_UserHandlerService_Once`
  - GetUserId() metodunun tam 1 kez çaðrýldýðýný doðrular
- ? `Handle_Should_Not_Set_Empty_AuthorId`
  - AuthorId'nin Guid.Empty olmadýðýný kontrol eder

### 2. **AuthorId Kullanýmý** (1 Test)
- ? `Handle_Should_Pass_Correct_AuthorId_To_TitleIsExist`
  - UserHandlerService'den gelen ID'nin `TitleIsExist()` metoduna doðru gönderildiðini doðrular

### 3. **Entity Mapping** (1 Test)
- ? `Handle_Should_Create_Valid_Blog_Entity`
  - Command'dan Blog entity'sine tüm alanlarýn doðru map'lendiðini kontrol eder

### 4. **Validation & Exception Handling** (2 Test)
- ? `Handle_Should_Throw_Exception_When_Validation_Fails`
  - BlogService.ValidateBlog() false döndüðünde exception fýrlatýr
- ? `Handle_Should_Throw_Exception_When_Title_Already_Exists`
  - Duplicate title durumunda exception fýrlatýr

### 5. **Result Kontrolü** (1 Test)
- ? `Handle_Should_Return_Blog_Id_In_Result`
  - CreateBlogResult'taki Id'nin oluþturulan Blog'un Id'si ile ayný olduðunu doðrular

## ??? Kullanýlan Teknolojiler

- **NUnit 4.3.2** - Test framework
- **NSubstitute 5.3.0** - Mocking library
- **FluentAssertions 7.0.0** - Okunabilir assertion'lar
- **MediatR 12.5.0** - CQRS pattern (IRequestHandler)

## ?? NSubstitute Özellikleri (Kullanýlanlar)

### 1. **Mock Oluþturma**
```csharp
_mockUserHandlerService = Substitute.For<IUserHandlerService>();
```

### 2. **Return Deðeri Ayarlama**
```csharp
_mockUserHandlerService.GetUserId().Returns(expectedUserId);
```

### 3. **Herhangi Bir Parametreyi Kabul Etme**
```csharp
_mockBlogService.ValidateBlog(Arg.Any<Blog>()).Returns(true);
```

### 4. **Metod Çaðrýsýný Yakalama (Arg.Do)**
```csharp
Blog? capturedBlog = null;
await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));

// Daha sonra capturedBlog nesnesini assert edebilirsiniz
capturedBlog.AuthorId.Should().Be(expectedUserId);
```

### 5. **Metod Çaðrýsýný Doðrulama**
```csharp
_mockUserHandlerService.Received(1).GetUserId();
```

### 6. **Async Metod Mock'lama**
```csharp
_mockBlogRepository.TitleIsExist(Arg.Any<Guid>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
    .Returns(Task.FromResult(false));
// veya kýsa hali:
    .Returns(false); // NSubstitute otomatik Task'e çevirir
```

### 7. **Belirli Parametrelerle Doðrulama**
```csharp
await _mockBlogRepository.Received(1).TitleIsExist(
    expectedUserId, // Belirli bir Guid
    command.Title,  // Belirli bir string
    Arg.Any<CancellationToken>()); // Herhangi bir CancellationToken
```

## ?? Testleri Çalýþtýrma

```bash
# Tüm testleri çalýþtýr
dotnet test tests\BlogApp.Application.UnitTests\BlogApp.Application.UnitTests.csproj

# Verbose output
dotnet test tests\BlogApp.Application.UnitTests\BlogApp.Application.UnitTests.csproj --verbosity normal

# Sadece belirli testi çalýþtýr
dotnet test --filter "FullyQualifiedName~Handle_Should_Set_AuthorId_From_UserHandlerService"
```

## ?? Test Anatomy (MediatR Handler)

### Handler Test Yapýsý
```csharp
[TestFixture]
public class CreateBlogCommandHandlerTests
{
    private IUserHandlerService _mockUserHandlerService;
    private CreateBlogCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        // Mock'larý oluþtur
        _mockUserHandlerService = Substitute.For<IUserHandlerService>();
        
        // Handler'ý inject et
        _handler = new CreateBlogCommandHandler(_mockUserHandlerService, ...);
    }

    [Test]
    public async Task Handle_Should_DoSomething()
    {
        // Arrange - Mock davranýþlarýný ayarla
        _mockUserHandlerService.GetUserId().Returns(Guid.NewGuid());
        
        // Act - Handler'ý çalýþtýr
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert - Sonuçlarý doðrula
        result.Should().NotBeNull();
        _mockUserHandlerService.Received(1).GetUserId();
    }
}
```

## ?? Best Practices (Uygulanmýþ)

### ? 1. Dependency Injection Test Edildi
```csharp
// UserHandlerService'in constructor'da inject edildiði ve kullanýldýðý doðrulandý
_mockUserHandlerService.Received(1).GetUserId();
```

### ? 2. Side Effects Yakalandý
```csharp
// AddAsync metoduna gönderilen Blog nesnesi yakalandý
Blog? capturedBlog = null;
await _mockBlogRepository.AddAsync(Arg.Do<Blog>(blog => capturedBlog = blog));
```

### ? 3. Ýç Metod Davranýþlarý Test Edildi
```csharp
// Private ToEntity() metodunun doðru çalýþtýðý dolaylý olarak test edildi
capturedBlog.Title.Should().Be(command.Title);
capturedBlog.AuthorId.Should().Be(expectedUserId); // ToEntity içinde set ediliyor
```

### ? 4. Tüm Execution Path'ler Test Edildi
- ? Happy path (baþarýlý senaryo)
- ? Validation failure
- ? Duplicate title
- ? AuthorId doðru set edildi

## ?? Debugging Ýpuçlarý

### Metod Çaðrýsýný Görmek
```csharp
// Kaç kez çaðrýldý?
_mockUserHandlerService.ReceivedCalls().Count().Should().Be(1);

// Hangi parametrelerle çaðrýldý?
await _mockBlogRepository.Received().TitleIsExist(
    Arg.Is<Guid>(g => g != Guid.Empty),
    Arg.Any<string>(),
    Arg.Any<CancellationToken>());
```

### Mock Çaðrýlarýný Yazdýrmak
```csharp
var calls = _mockUserHandlerService.ReceivedCalls();
foreach (var call in calls)
{
    Console.WriteLine($"Method: {call.GetMethodInfo().Name}");
    Console.WriteLine($"Arguments: {string.Join(", ", call.GetArguments())}");
}
```

## ?? Sonraki Adýmlar

- [ ] `UpdateBlogCommandHandler` testleri
- [ ] `DeleteBlogCommandHandler` testleri
- [ ] Query handler testleri (GetBlogQuery, GetAllBlogsQuery)
- [ ] Custom exception testleri
- [ ] Mapper testleri (eðer varsa)

## ?? Yakalanan Tasarým Ýyileþtirmeleri

### 1. Generic Exception Kullanýmý
**Mevcut:**
```csharp
if (!_blogService.ValidateBlog(blog))
    throw new Exception(); // Generic exception ?
```

**Öneri:**
```csharp
if (!_blogService.ValidateBlog(blog))
    throw new BlogValidationException("Blog validation failed"); // Custom exception ?
```

### 2. Blog Id'si Set Edilmiyor
**Mevcut:**
```csharp
return new Blog
{
    Title = request.Title,
    Content = request.Content,
    AuthorId = _userHandlerService.GetUserId(),
    // Id set edilmiyor, default Guid.Empty kalýyor
};
```

**Öneri:**
```csharp
return new Blog
{
    Id = Guid.NewGuid(), // Veya database'in set etmesini bekleyin
    Title = request.Title,
    Content = request.Content,
    AuthorId = _userHandlerService.GetUserId(),
};
```

## ?? Kaynaklar

- [NSubstitute Documentation](https://nsubstitute.github.io/)
- [MediatR Testing Guide](https://github.com/jbogard/MediatR/wiki)
- [FluentAssertions](https://fluentassertions.com/)
- [Unit Testing Best Practices](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
