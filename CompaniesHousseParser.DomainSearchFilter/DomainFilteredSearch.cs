using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Search;
using CompaniesHouseParser.Settings;
using Microsoft.Extensions.Logging;

namespace CompaniesHousseParser.DomainSearchFilter;

public class DomainFilteredSearch : IDomainFilteredSearch
{
    private IDomainSearch _domainSearch;
    private IApplicationCompanyFilter _applicationCompanyFilter;
    public ILogger<DomainFilteredSearch> Logger { get; set; }

    private string? _filterByNationality;

    public DomainFilteredSearch(
        IDomainSearch domainSearch,
        IApplicationSettingsAccessor applicationSettingsAccessor,
        ILogger<DomainFilteredSearch> logger)
    {
        _domainSearch = domainSearch;
        _applicationCompanyFilter = applicationSettingsAccessor.Get().Filters;
        Logger = logger;
    }

    public async Task<IDomainGetCompaniesResponse> GetFilteredCompaniesAsync()
    {
        var response = await GetNewCompaniesAsync();
        var companiesByNatioality = await FindByFilters(response.Companies);
        return new DomainGetCompaniesResponse
        {
            Companies = companiesByNatioality,
            CanFetchMoreCompanies = response.CanFetchMoreCompanies
        };
    }

    public async Task<ICompany> GetNotParsedCompanies(string companyId)
    {
        var company = await _domainSearch.GetCompanyByIdAsync(companyId);

        return company;
    }
    private async Task<IList<ICompany>> FindByFilters(IList<ICompany> companies)
    {
        InitializetFilters();
        var companiesWithFiltredOfficersByNationality = new List<ICompany>();
        foreach (var company in companies)
        {
            if (string.IsNullOrWhiteSpace(_filterByNationality))
            {
                companiesWithFiltredOfficersByNationality.Add(company);
                Logger.LogInformation(
                       $"Filters does not contain searching values. You scraping all companies.");
                continue;
            }

            if (await company.HasOfficerWithNationalityAsync(_filterByNationality))
            {
                companiesWithFiltredOfficersByNationality.Add(company);
            }

        }
        return companiesWithFiltredOfficersByNationality;
    }
    private async Task<IDomainGetCompaniesResponse> GetNewCompaniesAsync()
    {
        return await _domainSearch.GetNewlyIncorporatedCompaniesAsync();
    }

    private void InitializetFilters()
    {
        if (_filterByNationality != null)
        {
            return;
        }
        _filterByNationality = _applicationCompanyFilter.Officer.Nationality;
        Logger.LogInformation($"Searching by Nationality - {_filterByNationality} ");
    }
}
