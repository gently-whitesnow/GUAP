namespace Flow.StandardOperationError;

public class ForbiddenOperationError : OperationError
{
    public ForbiddenOperationError(string error, string message) : base(error, message)
    {
    }
}