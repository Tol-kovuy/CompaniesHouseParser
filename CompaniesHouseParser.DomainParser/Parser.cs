using CompaniesHouseParser.DomainShared;
using CompaniesHousseParser.DomainSearchFilter;

namespace CompaniesHouseParser.DomainParser;

public class Parser : IParser
{
    private IDomainFilteredSearch _domainFilteredSearch;
    private IDomainCompanyEmailSender _emailSender;

    public Parser(
        IDomainFilteredSearch domainFilteredSearch,
        IDomainCompanyEmailSender emailSender
        )
    {
        _domainFilteredSearch = domainFilteredSearch;
        _emailSender = emailSender;
    }

    public async Task ExecuteAsync()
    {
        var companies = await GetFilteredCompaniesAsync();
        if (companies.Count != 0)
        {
            foreach (var company in companies)
            {
                await _emailSender.SendAsync(company);
            }
            await ExecuteAsync();
        }
        else
            Console.WriteLine($"No newly created companies for at this time {DateTime.Now}");
    }

    private async Task<IList<ICompany>> GetFilteredCompaniesAsync()
    {
        return await _domainFilteredSearch.GetFilteredCompaniesAsync();
    }
}
