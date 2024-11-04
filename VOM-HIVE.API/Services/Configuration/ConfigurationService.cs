namespace VOM_HIVE.API.Services.Configuration
{
    public class ConfigurationService : IConfigurationInterface
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Audience { get; set; }

        public string Authority { get; set; }
    }
}
