using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestApi.Application.Configuration;
using RestApi.Application.Settings.Abstractions;

namespace RestApi.Infrastructure.Settings;

internal class SettingsGateway : ISettingsGateway
{
    private readonly IOptions<SettingsServiceOptions> _options;

    public SettingsGateway(IOptions<SettingsServiceOptions> options)
    {
        _options = options;
    }

    public async Task<Dictionary<string, string>> Get()
    {
        var response = await GetSettingsResponse();

        var body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InternalServiceException(InternalServices.Settings, new Exception(body));
        }

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(body);
    }

    private async Task<HttpResponseMessage> GetSettingsResponse()
    {
        using HttpClient client = new HttpClient
        {
            BaseAddress = new Uri(_options.Value.Url, UriKind.Absolute)
        };
        
        try
        {
            return await client.GetAsync("/settings");
        }
        catch (Exception e)
        {
            throw new InternalServiceException(InternalServices.Settings, e);
        }
    }
}
