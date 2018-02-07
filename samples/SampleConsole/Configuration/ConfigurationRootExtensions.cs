using System;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace TableStorage.UnsupportedTypes.SampleConsole.Configuration
{
    public static class ConfigurationRootExtensions
    {
        public static LogEventLevel GetLoggingLevel(this IConfigurationRoot configuration, string keyName,
            LogEventLevel defaultLevel = LogEventLevel.Warning)
        {
            try
            {
                return configuration.GetValue($"Logging:LogLevel:{keyName}", LogEventLevel.Warning);
            }
            catch (Exception)
            {
                return defaultLevel;
            }
        }
    }
}
