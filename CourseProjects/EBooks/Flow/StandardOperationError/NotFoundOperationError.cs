namespace Flow.StandardOperationError;

public class NotFoundOperationError : OperationError
{
    public NotFoundOperationError(string error, string message) : base(error, message)
    {
    }
}