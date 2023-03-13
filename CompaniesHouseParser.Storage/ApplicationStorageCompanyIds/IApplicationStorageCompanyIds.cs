using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage;

public interface IApplicationStorageCompanyIds : ITransientDependency
{
    void AddNewIds(IList<string> ids);
    IList<string> GetIds();
}