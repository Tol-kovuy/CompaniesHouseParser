using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.Settings;
using CompaniesHouseParser.Storage;
using System.Collections.Specialized;

namespace CompaniesHouseParser.Search;

// 1. Get incorporated date from storage
// 2. Get Companies from APi using incorporated date from 
// 3. Get list of all parsed companies from storage
// 4. Filter returned companies from API that was already parsed (use step 3.)
// 5. Save newly parsed companies id
// 6. Save last incorporated from id
// 7. return companies
public class DomainSearch
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

    private DateTime GetIncorporatedDate()
    {
        return _parsingStateAccessor.Get().Companies.LastIncorporatedFrom;
    }

    public async Task<IList<ICompany>> GetCompanies(DateTime incorporatedFrom)
    {
        var domainRequest = new DomainGetCompaniesRequest
        {
            IncorporatedFrom = incorporatedFrom
        };
        var getCompanies = await _domainCompaniesApi.GetCompaniesAsync(domainRequest);
        
        return getCompanies;
    }

    public void SaveNewCompanyValuesToFile(IList<ICompany> companies)
    {
        var ids = new List<string>();
        var dates = new List<DateTime>();
        foreach (var company in companies)
        {
            ids.Add(company.Id);
            dates.Add(company.CreatedDate);
        }
        _applicationStorageCompanyIds.AddNewIds(ids);
        _applicationStorageCreatedDate.AddRange(dates);
    }

    public DateTime GetLastDate(IList<DateTime> dates)
    {
        var datesList = new List<DateTime>();
        foreach (var date in dates)
        {
            datesList.Add(date);
        }
        var nowDate = DateTime.Now;
        var lastDate = datesList.OrderBy(date => Math.Abs((nowDate - date).TotalSeconds)).First();

        return lastDate;
    }
}
