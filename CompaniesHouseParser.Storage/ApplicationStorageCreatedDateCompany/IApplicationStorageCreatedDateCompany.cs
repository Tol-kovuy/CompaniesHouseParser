using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage;

public interface IApplicationStorageCreatedDateCompany : ITransientDependency
{
    DateTime GetSearchIncorporatedFromDate();
    void SetSearchIncorporatedFromDate(DateTime dates);
}
