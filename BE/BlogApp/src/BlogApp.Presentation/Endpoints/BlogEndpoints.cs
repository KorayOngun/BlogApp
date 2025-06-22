using BlogApp.Application.Commands.Blogs.Create;
using BlogApp.Application.Queries.Blog.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Endpoints;

public static class BlogEndpoints
{
    public static void MapBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/blogs", async ([FromBody] CreateBlogRequest request,
            IMediator mediator) =>
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command);
            return Results.Created("", result);
        })
        .WithName("CreateBlog")
        .Produces<BlogApp.Core.Entities.Blog>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);


        app.MapGet("/blogs/{id:guid}", async ([FromRoute] Guid id,
            IMediator mediator) =>
        {
            var query =  new GetBlogByIdRequest(id).ToQuery();
            var result = await mediator.Send(query);
            return result;
        });
    }
}


public record CreateBlogRequest(string Title, string Content)
{
    public CreateBlogCommand ToCommand()
    {
        return new CreateBlogCommand
        {
            Title = Title,
            Content = Content,
            AuthorId = Guid.NewGuid() // Assuming AuthorId is generated or passed in some way
        };
    }
}

public record GetBlogByIdRequest(Guid Id)
{
    public GetBlogByIdQuery ToQuery()
    {
        return new GetBlogByIdQuery
        {
            Id = Id
        };
    }
}