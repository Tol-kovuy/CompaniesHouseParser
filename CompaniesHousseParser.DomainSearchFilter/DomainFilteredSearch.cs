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
        private string _filterBy;

        public DomainFilteredSearch(
            IDomainSearch domainSearch,
            IApplicationSettingsAccessor applicationSettingsAccessor
            )
        {
            _domainSearch = domainSearch;
            // TODO: remove code duplicated use var
            _applicationCompanyFilter = applicationSettingsAccessor.Get().Filters;
            _companiesHouseApiRequestLimit = applicationSettingsAccessor.Get().CompaniesHouseApi.RequestLimit;
        }

        public async Task<IList<ICompany>> GetFilteredCompaniesAsync()
        {
            var allLastCompaniesWithOfficers = await GetNewlyIncorporatedCompaniesAsync(); //получаем все компании со всеми им принадлежащими офицерами
            var companiesByNatioality = new List<ICompany>();
            InitializetFilterByNationality();
            foreach (var company in allLastCompaniesWithOfficers)
            {
                foreach (var officers in await company.GetOfficersAsync()) //уже не обращаемся к АПИ, получаем закешированых офицеров
                {
                    if (officers.Nationality != null && officers.Nationality.Contains(_filterBy))
                    {
                        companiesByNatioality.Add(company);
                    }
                }
            }
            return companiesByNatioality;
        }

        private async Task<IList<ICompany>> GetNewlyIncorporatedCompaniesAsync()
        {
            var companies = await GetNewCompaniesAsync();
            var filteredCompanies = new List<ICompany>();
            foreach (var company in companies)
            {
                await company.GetOfficersAsync();
                InitializeRequestInterval();
                await Task.Delay(_requestInterval);
                filteredCompanies.Add(company);
            }
            return filteredCompanies;
        }

        

        private async Task<IList<ICompany>> GetNewCompaniesAsync()
        {
            return await _domainSearch.GetNewlyIncorporatedCompaniesAsync();
        }

        private TimeSpan InitializeRequestInterval()
        {
            if (_requestInterval != TimeSpan.Zero)
            {
                return _requestInterval;
            }
            _requestInterval = _companiesHouseApiRequestLimit.Interval;
            return _requestInterval;
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
}
