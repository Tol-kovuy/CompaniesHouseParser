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
        SaveNewCompanyValuesToFile(filterredCompanites);

        return filterredCompanites;
    }

    private async Task<IList<ICompany>> GetCompaniesAsync()
    {
        var domainRequest = new DomainGetCompaniesRequest
        {
            IncorporatedFrom = _applicationStorageCreatedDate.GetDate()
        };
        var getCompanies = await _domainCompaniesApi.GetCompaniesAsync(domainRequest);

        return getCompanies;
    }

    private IList<ICompany> FilterIncorporatedCompanies(IList<ICompany> newlyIncorporatedCompanies)
    {
        if (!File.Exists("ExistingCompanyNumbers.txt")) // first parsing 
        {
            var ids = new List<string>();
            foreach (var newlycompany in newlyIncorporatedCompanies)
            {
                ids.Add(newlycompany.Id);
            }
            _applicationStorageCompanyIds.AddNewIds(ids);

            return newlyIncorporatedCompanies;
        }
        else // second(n...) parsing 
        {
            var parsedCompaniesIds = _applicationStorageCompanyIds.GetIds();
            var filteredCompanies = new List<ICompany>();

            foreach (var newlycompany in newlyIncorporatedCompanies)
            {
                if (!(parsedCompaniesIds.Contains(newlycompany.Id)))
                {
                    filteredCompanies.Add(newlycompany);
                }
            }

            return filteredCompanies;
        }
    }

    private void SaveNewCompanyValuesToFile(IList<ICompany> companies)
    {
        var ids = new List<string>();
        var dates = new List<DateTime>();
        foreach (var company in companies)
        {
            ids.Add(company.Id);
            dates.Add(company.CreatedDate);
        }
        _applicationStorageCompanyIds.AddNewIds(ids);
        _applicationStorageCreatedDate.ReWriteIncorporatedFrom(dates);
    }
}
