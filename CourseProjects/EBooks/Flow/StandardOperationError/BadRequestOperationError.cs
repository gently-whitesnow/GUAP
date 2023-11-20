namespace Flow.StandardOperationError;

public class BadRequestOperationError : OperationError
{
    public BadRequestOperationError(string error, string message) : base(error, message)
    {
    }
}