namespace RestApi.Domain.Exceptions;

public enum InternalServices
{
    Settings = 0,
}

public class InternalServiceException : Exception
{
    public InternalServiceException(InternalServices service, Exception innerException)
        : base($"An error occurred on communication with {service} settings", innerException)
    {
    }
}
