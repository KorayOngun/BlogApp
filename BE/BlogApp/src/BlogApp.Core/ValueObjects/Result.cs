

namespace BlogApp.Core.ValueObjects;


public class Result
{

    public bool IsOk { get { return Success; } }

    public bool IsError { get { return !Success; } }
    public string? Message { get; }

    protected bool Success { get; }

    protected Result(bool success)
    {
        Success = success;
    }

    protected Result(bool success, string? message)
    {
        Success = success;
        Message = message;
    }


    public static Result Error() => new(false);

    public static Result Error(string message) => new(false, message);

    public static Result Ok(string message) => new(true, message);
    public static Result Ok() => new(true);

    public Error AsError() => new(Message);
}


public sealed class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool success, T? value = default, string? message = null)
        : base(success, message)
    {
        Value = value;
    }

    public static Result<T> Ok(T value)
        => new(true, value);


    public bool TryGetValue(out T value)
    {
        if (IsOk && Value is not null)
        {
            value = Value;
            return true;
        }

        value = default!;
        return false;
    }

    public static implicit operator Result<T>(Error e) => new(false, default, e.Message);
    public static implicit operator Result<T>(T data) => new(true, data);
}

public record Error(string? Message);
