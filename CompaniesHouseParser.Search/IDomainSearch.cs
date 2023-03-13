using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Search
{
    public interface IDomainSearch : ITransientDependency
    {
        Task<IList<ICompany>> GetNewlyIncorporatedCompaniesAsync();
    }
}