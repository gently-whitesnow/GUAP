namespace Flow;

public record struct Operation<T>
{
    public bool IsSuccess { get; private init; }
    public bool IsNotSuccess => !IsSuccess;
    public T Value { get; private init; }
    public IOperationError Error { get; private set; }
    
    internal static Operation<T> Failed(IOperationError error)
    {
        if (error == null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        return new Operation<T>
        {
            Error = error,
            IsSuccess = false
        };
    }

    public Operation<TTarget> FlowError<TTarget>()
    {
        return Operation<TTarget>.Failed(Error);
    }

    public Operation FlowError()
    {
        return Operation.Failed(Error);
    }

    internal static Operation<T> Success(T value)
    {
        return new Operation<T>
        {
            Value = value,
            IsSuccess = true
        };
    }

    public static implicit operator Operation<T>(T result)
    {
        return Success(result);
    }
}

public record struct Operation
{
    public IOperationError Error { get; private set; }

    public bool IsSuccess { get; private set; }
    public bool IsNotSuccess => !IsSuccess;

    public Operation<TTarget> FlowError<TTarget>()
    {
        return Operation<TTarget>.Failed(Error);
    }

    public Operation FlowError()
    {
        return this;
    }

    internal static Operation Failed(IOperationError error)
    {
        if (error == null)
        {
            throw new ArgumentNullException(nameof(error));
        }

        return new Operation
        {
            Error = error,
            IsSuccess = false
        };
    }

    internal static Operation Success()
    {
        return new Operation
        {
            IsSuccess = true
        };
    }
}
