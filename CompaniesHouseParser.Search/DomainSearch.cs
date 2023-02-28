using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.Storage;

namespace CompaniesHouseParser.Search;

public class DomainSearch : IDomainSearch
{
    private readonly IDomainCompaniesApi _domainCompaniesApi;
    private readonly ICompanyHouseParsingStateAccessor _parsingStateAccessor;
    private readonly IApplicationStorageCompanyIds _applicationStorageCompanyIds;
    private readonly IApplicationStorageCreatedDateCompany _applicationStorageCreatedDate;

    public DomainSearch(IDomainCompaniesApi domainCompaniesApi,
        ICompanyHouseParsingStateAccessor parsingStateAccessor,
        IApplicationStorageCompanyIds applicationStorageCompanyIds,
        IApplicationStorageCreatedDateCompany applicationStorageCreatedDate)
    {
        _domainCompaniesApi = domainCompaniesApi;
        _parsingStateAccessor = parsingStateAccessor;
        _applicationStorageCompanyIds = applicationStorageCompanyIds;
        _applicationStorageCreatedDate = applicationStorageCreatedDate;
    }

    public async Task<IList<ICompany>> GetNewlyIncorporatedCompanies()
    {
        var newlyIncorporatedCompanies = await GetCompaniesAsync();
        var filterredCompanites = FilterIncorporatedCompanies(newlyIncorporatedCompanies);
        SaveNewIds(filterredCompanites);
        SaveNewDate(filterredCompanites);
        return filterredCompanites;
    }

    private async Task<IList<ICompany>> GetCompaniesAsync()
    {
        var domainRequest = new DomainGetCompaniesRequest
        {
            IncorporatedFrom = _applicationStorageCreatedDate.GetLastIncorporatedFromDate()
        };
        var getCompanies = await _domainCompaniesApi.GetCompaniesAsync(domainRequest);
        return getCompanies;
    }

    private IList<ICompany> FilterIncorporatedCompanies(IList<ICompany> newlyIncorporatedCompanies)
    {
        var parsedCompaniesIds = _applicationStorageCompanyIds.GetIds();
        var filteredCompanies = newlyIncorporatedCompanies
            .Where(company => !parsedCompaniesIds.Contains(company.Id)).ToList();
        return filteredCompanies;
    }

    private void SaveNewIds(IList<ICompany> companies)
    {
        int i = 1;
        var ids = companies.Select(company => $"{i++} {company.Id}").ToList();
        _applicationStorageCompanyIds.AddNewIds(ids);
    }

    private void SaveNewDate(IList<ICompany> companies)
    {
        var date = companies.Max(d => d.CreatedDate);
        _applicationStorageCreatedDate.SetLastIncorporatedFromDate(date);
    }
}
