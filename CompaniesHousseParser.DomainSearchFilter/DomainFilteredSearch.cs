using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Search;
using CompaniesHouseParser.Settings;

namespace CompaniesHousseParser.DomainSearchFilter;

public class DomainFilteredSearch : IDomainFilteredSearch
{
    private IDomainSearch _domainSearch;
    private IApplicationCompanyFilter _applicationCompanyFilter;
    private string _filterBy;

    public DomainFilteredSearch(
        IDomainSearch domainSearch,
        IApplicationSettingsAccessor applicationSettingsAccessor
        )
    {
        _domainSearch = domainSearch;
        _applicationCompanyFilter = applicationSettingsAccessor.Get().Filters;
    }

    public async Task<IList<ICompany>> GetFilteredCompaniesAsync()
    {
        var companies = await GetNewCompaniesAsync();
        //var companyWithOfficers = await HasOfficerWithNationalityAsync(companies);
        var companiesByNatioality = await FindByNationality(companies);
        return companiesByNatioality;
    }

    private async Task<IList<ICompany>> FindByNationality(IList<ICompany> companies)
    {
        InitializetFilterByNationality();
        var companiesWithFiltredOfficersByNationality = new List<ICompany>();
        foreach (var company in companies)
        {
            if (await company.HasOfficerWithNationalityAsync(_filterBy))
            {
                companiesWithFiltredOfficersByNationality.Add(company);
            }

        }
        return companiesWithFiltredOfficersByNationality;
    }
    private async Task<IList<ICompany>> GetNewCompaniesAsync()
    {
        return await _domainSearch.GetNewlyIncorporatedCompaniesAsync();
    }

    private void InitializetFilterByNationality()
    {
        if (_filterBy != null)
        {
            return;
        }
        _filterBy = _applicationCompanyFilter.Officer.Nationality;
    }
}
