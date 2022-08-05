namespace RestApi.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(Type type, string id) : this(type, id, null!)
    {
    }

    public EntityNotFoundException(Type type, string id, Exception innerException)
        : base($"{type.Name} with id {id} was not found", innerException)
    {
    }
}
