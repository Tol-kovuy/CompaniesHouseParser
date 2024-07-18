using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHousseParser.DomainSearchFilter;

public interface IDomainFilteredSearch : ITransientDependency
{
    Task<IDomainGetCompaniesResponse> GetFilteredCompaniesAsync();
    Task<ICompany> GetNotParsedCompanies(string companyId);
}
