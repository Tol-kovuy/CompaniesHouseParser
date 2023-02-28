using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.Storage;
using System.Collections.Generic;

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
        SaveNewCompanyIdsToFile(filterredCompanites);
        SaveNewCompanyCreatedDateToFile(filterredCompanites);
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
                if (parsedCompaniesIds.Contains(newlycompany.Id))
                {
                    continue;
                }
                filteredCompanies.Add(newlycompany);
            }
            return filteredCompanies;
        }
    }

    private void SaveNewCompanyIdsToFile(IList<ICompany> companies)
    {
        var ids = new List<string>();
        foreach (var company in companies)
        {
            ids.Add(company.Id);
        }
        _applicationStorageCompanyIds.AddNewIds(ids);
    }

    private void SaveNewCompanyCreatedDateToFile(IList<ICompany> companies)
    {
        var date = companies.Max(d => d.CreatedDate);
        _applicationStorageCreatedDate.ReWriteIncorporatedDateFrom(date);
    }
}
