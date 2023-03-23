using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainParser;

public interface IParser : ITransientDependency
{
    Task ExecuteAsync();
}
