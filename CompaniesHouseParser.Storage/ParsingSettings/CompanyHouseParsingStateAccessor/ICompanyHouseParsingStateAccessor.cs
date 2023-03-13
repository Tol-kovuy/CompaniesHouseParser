using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage;

public interface ICompanyHouseParsingStateAccessor : ITransientDependency
{
    IApplicationParsingState Get();
}