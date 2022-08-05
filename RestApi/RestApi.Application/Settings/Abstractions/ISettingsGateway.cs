namespace RestApi.Application.Settings.Abstractions;

public interface ISettingsGateway
{
    Task<Dictionary<string, string>> Get();
}
