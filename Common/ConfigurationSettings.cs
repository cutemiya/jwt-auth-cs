using Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Common;

public class ConfigurationSettings : IConfigurationSettings
{
    private readonly IConfiguration _configuration;

    public ConfigurationSettings(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public string DbConnectionsOwn => _configuration.GetSection("Postgres").Value;

}