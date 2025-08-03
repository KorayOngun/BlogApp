namespace BlogApp.Presentation.Endpoints;

public static class Endpoints
{
    public static void MapEndpoints(this IEndpointRouteBuilder app)
    {
        app
           .MapGroup("Blog")
           .WithTags("Blogs")
           .MapBlogEndpoints();
           
           
    }
}
