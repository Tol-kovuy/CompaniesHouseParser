using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.IoC;

namespace CompaniesHousseParser.DomainSearchFilter;

public interface IDomainFilteredSearch : ITransientDependency
{
    Task<IDomainGetCompaniesResponse> GetFilteredCompaniesAsync();
}
