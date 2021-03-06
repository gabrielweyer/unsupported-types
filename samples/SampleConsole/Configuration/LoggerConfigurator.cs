﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TableStorage.UnsupportedTypes.SampleConsole.Configuration
{
    public static class LoggerConfigurator
    {
        public static ILoggerFactory ConfigureSerilog(this IConfigurationRoot configuration)
        {
            var serilogLevel = configuration.GetLoggingLevel("Serilog");

            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Is(serilogLevel)
                .WriteTo.Console(serilogLevel);

            var logger = loggerConfiguration.CreateLogger();

            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddSerilog(logger);

            return loggerFactory;
        }
    }
}
