namespace RestApi.Domain.Exceptions;

public class EntityValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public EntityValidationException(Dictionary<string, string[]> errors) : base("Validation exception")
    {
        Errors = errors;
    }
}
