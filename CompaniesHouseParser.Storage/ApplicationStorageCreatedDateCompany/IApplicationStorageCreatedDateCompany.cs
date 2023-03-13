using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Storage
{
    public interface IApplicationStorageCreatedDateCompany : ITransientDependency
    {
        DateTime GetLastIncorporatedFromDate();
        void SetLastIncorporatedFromDate(DateTime dates);
    }
}
