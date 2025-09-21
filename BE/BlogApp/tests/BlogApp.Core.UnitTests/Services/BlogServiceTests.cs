using System.Threading.Tasks;
using BlogApp.Core.Entities;
using BlogApp.Core.Repository;
using BlogApp.Core.Services;
using NSubstitute;

namespace BlogApp.Core.UnitTests.Services;

public class BlogServiceTests
{
    private readonly IBlogRepository _blogRepository = Substitute.For<IBlogRepository>();

    private readonly BlogService _sut;

    public BlogServiceTests()
    {
        _sut = new BlogService(_blogRepository);
    }


    [Test]
    public async Task AddAsync_Should_Throw_When_Title_Is_Duplicate()
    {
        Assert.Pass();
    }
}
