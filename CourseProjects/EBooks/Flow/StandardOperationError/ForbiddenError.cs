namespace Flow.StandardOperationError;

public record ForbiddenError(string Error, string Message) : Error;