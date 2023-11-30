namespace Flow.StandardOperationError;

public record NotFoundError(string Error, string Message) : Error;