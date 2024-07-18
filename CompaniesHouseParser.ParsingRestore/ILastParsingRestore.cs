using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.ParsingRestore
{
    public interface ILastParsingRestore : ISingletonDependency
    {
        Task WriteNorParsedCompaniesAsync();
        Task<IDomainGetCompaniesResponse> GetFilteredCompaniesAsync();
        Task WriteParsedOfficersToResultAsync(IList<ICompany> notParsedCompanies);
    }
}
