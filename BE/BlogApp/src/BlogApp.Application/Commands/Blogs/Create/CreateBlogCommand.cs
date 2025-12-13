using BlogApp.MessageContracts.Requests.Blogs;
using BlogApp.MessageContracts.Responses.Blogs;
using MediatR;

namespace BlogApp.Application.Commands.Blogs.Create;

public class CreateBlogCommand(CreateBlogRequest request) : IRequest<CreateBlogResponse>
{
    public CreateBlogRequest Request => request;
}
