namespace VOM_HIVE.API.Services.Configuration
{
    public interface IConfigurationInterface
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string Audience { get; }
        string Authority { get; }
    }
}
