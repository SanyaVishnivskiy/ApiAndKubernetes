namespace RestApi.Common;

public class ServiceId
{
    private static Guid _id = Guid.NewGuid();

    public Guid Id => _id;
}
