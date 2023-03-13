using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage;

public interface IApplicationCompaniesParsingState : ITransientDependency
{
    DateTime LastIncorporatedFrom { get; set; }
}