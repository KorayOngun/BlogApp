﻿using BlogApp.Application.Queries.Blog.GetById;
using BlogApp.MessageContracts.Requests.Blogs;
using BlogApp.Presentation.Mappers.Blogs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Presentation.Endpoints;

public static class BlogEndpoints
{
    public static IEndpointRouteBuilder MapBlogEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromBody] CreateBlogRequest request,
            IMediator mediator) =>
        {
            var command = request.ToCommand();
            var result = await mediator.Send(command);
            return Results.Ok(result.ToResponse());
        })
        .WithName("CreateBlog");
            

        app.MapGet("/{id:guid}", async ([FromRoute] Guid id,
            IMediator mediator) =>
        {
            var query = new GetBlogByIdQuery() { Id = id };
            var result = await mediator.Send(query);
            return result;
        })
        .WithName("GetById");
        
        return app;
    }
}

