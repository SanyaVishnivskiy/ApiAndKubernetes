using RestApi.Application.Settings.Abstractions;

namespace RestApi.Application.Settings.Queries.Get;

public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, Dictionary<string, string>>
{
    private readonly ISettingsGateway _gateway;

    public GetSettingsQueryHandler(ISettingsGateway gateway)
    {
        _gateway = gateway;
    }

    public async Task<Dictionary<string, string>> Handle(GetSettingsQuery request, CancellationToken cancellationToken)
    {
        return await _gateway.Get();
    }
}
