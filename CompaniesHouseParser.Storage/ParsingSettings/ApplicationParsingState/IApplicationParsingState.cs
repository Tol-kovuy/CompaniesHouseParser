using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage;

public interface IApplicationParsingState : ITransientDependency
{
    IApplicationCompaniesParsingState Companies { get; }
}