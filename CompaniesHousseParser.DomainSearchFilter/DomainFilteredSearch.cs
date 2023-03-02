using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Search;
using CompaniesHouseParser.Settings;

namespace CompaniesHousseParser.DomainSearchFilter
{
    public class DomainFilteredSearch : IDomainFilteredSearch
    {
        private IDomainSearch _domainSearch;
        private IApplicationCompanyFilter _applicationCompanyFilter;
        private ICompaniesHouseApiRequestLimit _companiesHouseApiRequestLimit;
        private TimeSpan _requestInterval;

        public DomainFilteredSearch(
            IDomainSearch domainSearch,
            IApplicationSettingsAccessor applicationSettingsAccessor
            )
        {
            _domainSearch = domainSearch;
            _applicationCompanyFilter = applicationSettingsAccessor.Get().Filters;
            _companiesHouseApiRequestLimit = applicationSettingsAccessor.Get().CompaniesHouseApi.RequestLimit;
        }

        public async Task<IList<ICompany>> GetNewlyIncorporatedCompaniesAsync()
        {
            var companies = await GetNewCompaniesAsync();
            var filteredCompanies = new List<ICompany>();
            foreach (var company in companies)
            {
                var filterByNationality = _applicationCompanyFilter.Officer.Nationality;
                var officers = await company.GetOfficersAsync();
                GetRequestInterval();
                await Task.Delay(_requestInterval);
                foreach (var officer in officers)
                {
                    if (officer.Nationality.Contains(filterByNationality))
                    {
                        filteredCompanies.Add(company);
                    }
                }
            }
            return filteredCompanies;
        }

        private async Task<IList<ICompany>> GetNewCompaniesAsync()
        {
            return await _domainSearch.GetNewlyIncorporatedCompaniesAsync();
        }

        private async Task<IList<IOfficer>> GetOfficersByNationalityAsync()
        {
            var companies = await GetNewCompaniesAsync();
            var listOfficers = new List<IOfficer>();
            foreach (var company in companies)
            {
                GetRequestInterval();
                await Task.Delay(_requestInterval);
                var officer = await company.GetOfficersAsync();
                listOfficers.AddRange(officer);
            }
            return listOfficers;
        }

        private TimeSpan GetRequestInterval()
        {
            if (_requestInterval != TimeSpan.Zero)
            {
                return _requestInterval;
            }
            _requestInterval = _companiesHouseApiRequestLimit.Interval;
            return _requestInterval;
        }
    }
}
