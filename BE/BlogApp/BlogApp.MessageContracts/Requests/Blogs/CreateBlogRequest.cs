namespace BlogApp.MessageContracts.Requests.Blogs;

public record CreateBlogRequest
{
    public string Title { get; init; }
    public string Content { get; init; }
}
