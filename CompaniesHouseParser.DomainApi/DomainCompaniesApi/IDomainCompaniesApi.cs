using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainApi;

public interface IDomainCompaniesApi : ITransientDependency
{
    Task<IDomainGetCompaniesResponse> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi);
    Task<ICompany> GetCompanyByIdAsync(string id);
}
