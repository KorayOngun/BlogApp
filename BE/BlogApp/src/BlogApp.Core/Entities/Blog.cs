namespace BlogApp.Core.Entities;

public class Blog : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required Guid AuthorId { get; set; }
}
