namespace Books.Api.Exceptions;

public class ValidationAppException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationAppException(Dictionary<string, string[]> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }
}
