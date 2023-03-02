using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Settings;

namespace CompaniesHousseParser.DomainSearchFilter
{
    public interface IDomainFilteredSearch
    {
        Task<IList<ICompany>> GetNewlyIncorporatedCompaniesAsync();
    }
}
