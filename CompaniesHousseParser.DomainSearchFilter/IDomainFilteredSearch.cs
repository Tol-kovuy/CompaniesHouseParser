using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHousseParser.DomainSearchFilter;

public interface IDomainFilteredSearch : ITransientDependency
{
    Task<IList<ICompany>> GetFilteredCompaniesAsync();
}
