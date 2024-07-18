using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Search;

public interface IDomainSearch : ITransientDependency
{
    Task<IDomainGetCompaniesResponse> GetNewlyIncorporatedCompaniesAsync();
    Task<ICompany> GetCompanyByIdAsync(string companyId);
}