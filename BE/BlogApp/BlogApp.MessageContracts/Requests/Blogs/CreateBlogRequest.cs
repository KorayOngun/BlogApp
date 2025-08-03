namespace BlogApp.MessageContracts.Requests.Blogs;

public record CreateBlogRequest
{
    public required string Title { get; init; }
    public required string Content { get; init; }
}
