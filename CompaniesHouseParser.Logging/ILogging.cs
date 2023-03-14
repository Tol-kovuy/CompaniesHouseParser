using CompaniesHouseParser.IoC;
using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.Logging
{
    public interface ILogging : ISingletonDependency 
    {
        void GetLogger<T>(LogLevel logLevel, string message);
    }
}