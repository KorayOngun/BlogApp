using BlogApp.Application.Queries.Blog.GetById;
using BlogApp.MessageContracts.Requests.Blogs;
using BlogApp.Presentation.Mappers.Blogs;
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
            return Results.Ok(result.ToResponse());
        })
        .WithName("CreateBlog");


        app.MapGet("/blogs/{id:guid}", async ([FromRoute] Guid id,
            IMediator mediator) =>
        {
            var query =  new GetBlogByIdRequest(id).ToQuery();
            var result = await mediator.Send(query);
            return result;
        });
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