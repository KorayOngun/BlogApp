namespace BlogApp.MessageContracts;
public record PagedRequest
{
    public int Page { get; set; }
    public int Size { get; set; }
}

public record PagedResponse<T>
{
    public T[] Items { get; set; } = [];
}