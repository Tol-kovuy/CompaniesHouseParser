using CompaniesHouseParser.DomainShared;
using CompaniesHousseParser.DomainSearchFilter;
using Microsoft.Extensions.Logging;

namespace CompaniesHouseParser.DomainParser;

public class Parser : IParser
{
    private IDomainFilteredSearch _domainFilteredSearch;
    private IDomainCompanyEmailSender _emailSender;
    public ILogger Logger { get; set; }

    public Parser(
        ILogger<Parser> logger,
        IDomainFilteredSearch domainFilteredSearch,
        IDomainCompanyEmailSender emailSender
        )
    {
        Logger = logger;
        _domainFilteredSearch = domainFilteredSearch;
        _emailSender = emailSender;
    }

    public async Task ExecuteAsync()
    {
        var companies = await GetFilteredCompaniesAsync();
        if (companies.Count == 0)
        {
            Logger.LogInformation(
                $"No newly created companies for at this time {DateTime.Now}");
        }
        else
        {
            foreach (var company in companies)
            {
                await _emailSender.SendAsync(company);
            }
            await ExecuteAsync();
        }
    }

    private async Task<IList<ICompany>> GetFilteredCompaniesAsync()
    {
        return await _domainFilteredSearch.GetFilteredCompaniesAsync();
    }
}
