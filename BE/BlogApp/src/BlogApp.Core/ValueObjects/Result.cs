

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

    protected Result(bool success, string message)
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


public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(true)
    {
        Value = value;
    }

    private Result(bool success) : base(success) { }

    private Result(bool success, string message) : base(success, message) { }

    public static new Result<T> Error() => new(false);

    public static new Result<T> Error(string message) => new(false, message);

    public static implicit operator Result<T>(T data) => new(data);

    public static implicit operator bool(Result<T> result) => result.Success;
    public static implicit operator Result<T>(Error error) => !string.IsNullOrEmpty(error.message) ? new(false, error.message) : new(false);
}

public record Error(string? message) 
{
    public string? Message { get; }
};