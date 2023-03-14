using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.Logging
{
    public class Logging : ILogging
    {
        public void GetLogger<T>(LogLevel logLevel, string message)
        {
            ILogger logger = LoggerFactory.Create(builder => builder.AddConsole())
            .CreateLogger<T>();

            logger.Log(logLevel, message);
        }
    }
}