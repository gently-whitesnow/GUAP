namespace Flow.StandardOperationError;

public abstract class OperationError : IOperationError
{
    public OperationError(string error, string message)
    {
        Error = error;
        Message = message;
    }
    public string Error { get; init; }
    public string Message { get; init; }
}