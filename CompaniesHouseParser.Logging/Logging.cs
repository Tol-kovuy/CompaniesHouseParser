using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.Logging
{
    public class Logging : ILogging
    {
        private ILogger _logger;
        public void GetLogger<T>(LogLevel logLevel, string message)
        {
            _logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger<T>();

            _logger.Log(logLevel, message);
        }
    }
}