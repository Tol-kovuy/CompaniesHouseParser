using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainApi
{
    public interface IDomainCompaniesApi : ITransientDependency
    {
        Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi);
    }
}
