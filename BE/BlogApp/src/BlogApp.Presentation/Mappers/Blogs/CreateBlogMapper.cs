using BlogApp.Application.Commands.Blogs.Create;
using BlogApp.MessageContracts.Requests.Blogs;
using BlogApp.MessageContracts.Responses.Blogs;
using Riok.Mapperly.Abstractions;

namespace BlogApp.Presentation.Mappers.Blogs;


[Mapper]
public static partial class CreateBlogMapper
{
    public static partial CreateBlogCommand ToCommand(this CreateBlogRequest request);
    public static partial CreateBlogResponse ToResponse(this CreateBlogResult result);
}
