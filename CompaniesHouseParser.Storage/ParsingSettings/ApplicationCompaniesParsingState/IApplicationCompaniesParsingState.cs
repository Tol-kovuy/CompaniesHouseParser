using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage;

public interface IApplicationCompaniesParsingState : ITransientDependency
{
    DateTime SearchIncorporatedFrom { get; set; }
}