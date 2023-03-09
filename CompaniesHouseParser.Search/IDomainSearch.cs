using CompaniesHouseParser.DomainShared;

namespace CompaniesHouseParser.Search
{
    public interface IDomainSearch
    {
        Task<IList<ICompany>> GetNewlyIncorporatedCompaniesAsync();
    }
}