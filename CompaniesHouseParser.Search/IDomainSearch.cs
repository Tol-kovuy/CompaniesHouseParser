using CompaniesHouseParser.DomainApi;

namespace CompaniesHouseParser.Search
{
    public interface IDomainSearch
    {
        Task<IList<ICompany>> GetNewlyIncorporatedCompanies();
    }
}