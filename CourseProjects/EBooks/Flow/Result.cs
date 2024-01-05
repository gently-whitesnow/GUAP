namespace Flow;

public record Result
{
    public Error? Error { get; init; }

    public bool IsSuccess => !HasError;
    public bool HasError { get; init; }

    protected Result()
    {
    }

    public Result(Error error)
    {
        Error = error;
        HasError = true;
    }
    
    public static readonly Result Success = new();
    public static implicit operator Result(Error error) => new(error);
}

public sealed record Result<TValue> : Result
{
    public TValue? Value { get; init; }

    public Result(TValue value)
    {
        Value = value;
    }

    public Result(Error error)
    {
        Error = error;
        HasError = true;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Error error) => new(error);
}