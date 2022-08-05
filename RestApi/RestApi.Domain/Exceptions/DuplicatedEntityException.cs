namespace RestApi.Domain.Exceptions;

public class DuplicatedEntityException : Exception
{
    public DuplicatedEntityException() : base("Entity is a duplicate") { }
}
