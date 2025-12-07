# BlogApp.Core.UnitTests

NUnit kullanýlarak yazýlmýþ BlogApp.Core katmaný için unit test suite'i.

## ?? Test Kapsamý

- ? **23 Test** - Tümü baþarýlý
- ? **0.9s** - Test süresi
- ?? **3 Test Kategorisi**

## ?? Test Kategorileri

### 1. Entity Tests (`BlogEntityTests.cs`)
Blog entity'sinin temel özelliklerini test eder:
- ? Property assignment
- ? BaseEntity inheritance
- ? Id, CreatedAt, UpdatedAt özellikleri
- ? Çeþitli title uzunluklarý
- ? Uzun content testi

**Test Sayýsý:** 6

### 2. Service Tests (`BlogServiceTests.cs`)
BlogService validation logic'ini test eder:
- ? Geçerli blog validation
- ? Empty AuthorId kontrolü
- ? Invalid title validasyonu (empty, whitespace)
- ? Null title kontrolü
- ? Birden fazla invalid field
- ? Content validation (þu an yok)

**Test Sayýsý:** 8

### 3. Base Entity Tests (`BaseEntityTests.cs`)
BaseEntity temel özellikleri:
- ? Id property
- ? CreatedAt property
- ? UpdatedAt property
- ? Default deðerler
- ? Inheritance kontrolü

**Test Sayýsý:** 7

## ??? Kullanýlan Teknolojiler

- **NUnit 3.14.0** - Test framework
- **FluentAssertions 7.0.0** - Okunabilir assertion'lar
- **NSubstitute 5.3.0** - Mocking (ileride kullanýlacak)
- **Coverlet** - Code coverage

## ?? Testleri Çalýþtýrma

### Tüm Testler
```bash
dotnet test tests\BlogApp.Core.UnitTests\BlogApp.Core.UnitTests.csproj
```

### Verbose Output
```bash
dotnet test tests\BlogApp.Core.UnitTests\BlogApp.Core.UnitTests.csproj --verbosity normal
```

### Coverage ile
```bash
dotnet test tests\BlogApp.Core.UnitTests\BlogApp.Core.UnitTests.csproj /p:CollectCoverage=true
```

## ?? Test Anatomisi (NUnit)

### Temel Test Yapýsý
```csharp
[Test]
public void MethodName_Scenario_ExpectedBehavior()
{
    // Arrange - Test verilerini hazýrla
    var input = CreateTestData();

    // Act - Test edilecek metodu çalýþtýr
    var result = _service.DoSomething(input);

    // Assert - Sonuçlarý doðrula
    result.Should().Be(expectedValue);
}
```

### SetUp ve TearDown
```csharp
[SetUp]
public void Setup()
{
    // Her test öncesi çalýþýr
    _service = new MyService();
}

[TearDown]
public void TearDown()
{
    // Her test sonrasý çalýþýr (cleanup)
}
```

### TestCase (Parametrik Test)
```csharp
[TestCase("")]
[TestCase("   ")]
[TestCase("invalid")]
public void Test_WithMultipleInputs(string input)
{
    // Her TestCase için ayrý ayrý çalýþýr
}
```

### FluentAssertions Örnekleri
```csharp
// Boolean
result.Should().BeTrue();
result.Should().BeFalse("custom reason");

// Strings
text.Should().Be("expected");
text.Should().NotBeNullOrEmpty();
text.Should().Contain("substring");

// Collections
list.Should().HaveCount(5);
list.Should().ContainSingle();
list.Should().BeEmpty();

// Dates
date.Should().BeAfter(otherDate);
date.Should().BeBefore(DateTime.Now);

// Exceptions
Action act = () => service.ThrowException();
act.Should().Throw<ArgumentException>()
   .WithMessage("Invalid argument");
```

## ?? Test Best Practices (Uygulananlar)

### ? 1. AAA Pattern (Arrange-Act-Assert)
Her test üç bölüme ayrýlmýþ:
```csharp
// Arrange - Hazýrlýk
var blog = new Blog { ... };

// Act - Ýþlem
var result = _service.ValidateBlog(blog);

// Assert - Doðrulama
result.Should().BeTrue();
```

### ? 2. Descriptive Test Names
```csharp
ValidateBlog_WithEmptyAuthorId_Should_Return_False()
Blog_Creation_Should_Set_Properties_Correctly()
```

### ? 3. Single Responsibility
Her test **tek bir davranýþý** test eder.

### ? 4. Independent Tests
Testler birbirinden baðýmsýz çalýþýr.

### ? 5. Fast Tests
Tüm suite 1 saniyeden kýsa sürede çalýþýr.

## ?? Test-Driven Development (TDD) Örneði

Bu projede TDD uygulandý:

**Durum:** `BlogService.ValidateBlog()` whitespace kontrolü yapmýyordu.

1. ? **Test yazýldý:**
```csharp
[TestCase("   ")]
public void ValidateBlog_WithInvalidTitle_Should_Return_False(string invalidTitle)
{
    var result = _blogService.ValidateBlog(blog);
    result.Should().BeFalse(); // FAILED!
}
```

2. ? **Kod düzeltildi:**
```csharp
// string.IsNullOrEmpty() yerine
if(string.IsNullOrWhiteSpace(blog.Title))
    return false;
```

3. ? **Test geçti:** Tüm testler baþarýlý

## ?? Sonraki Adýmlar

- [ ] Integration testleri ekle
- [ ] Repository testleri (Persistence layer)
- [ ] Command/Query handler testleri (Application layer)
- [ ] Architecture tests (ArchUnitNET)
- [ ] Code coverage %80+ hedef

## ?? Kaynaklar

- [NUnit Documentation](https://docs.nunit.org/)
- [FluentAssertions](https://fluentassertions.com/)
- [Clean Architecture Testing](https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/test-asp-net-core-mvc-apps)
