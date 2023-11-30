namespace Flow.StandardOperationError;

public record BadRequestError(string Error, string Message) : Error;