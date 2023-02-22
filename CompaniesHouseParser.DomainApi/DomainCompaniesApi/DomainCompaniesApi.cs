using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi
{
    public class DomainCompaniesApi : IDomainCompaniesApi
    {
        private ICompaniesHouseApi _companiesHouseApi;
        private IApplicationSettingsAccessor _settingsAccessor;

        public DomainCompaniesApi(
            ICompaniesHouseApi companiesHouseApi,
            IApplicationSettingsAccessor settingsAccessor
            )
        {
            _companiesHouseApi = companiesHouseApi;
            _settingsAccessor = settingsAccessor;
        }

        // 1. Get incorporated date from storage
        // 2. Get Companies from APi using incorporated date from 
        // 3. Get list of all parsed companies from storage
        // 4. Filter returned companies from API that was already parsed (use step 3.)
        // 5. Save newly prased companies id
        // 6. Save last incorporated from id
        // 7. return companies

        public async Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi)
        {
            var request = new GetAllCompaniesRequest
            {
                // TODO:  _settingsAccessor.Get().CompaniesHouseApi;
                ApiToken = _settingsAccessor.Get().CompaniesHouseApi.Token,
                CompaniesCount = _settingsAccessor.Get().CompaniesHouseApi.CompaniesCount,
                IncorporatedFrom = requestApi.IncorporatedFrom
            };

            var companiesDtos = await _companiesHouseApi.GetCompaniesAsync(request);

            var companies = new List<ICompany>();
            foreach (var companyFromDto in companiesDtos)
            {
                var company = new Company(_companiesHouseApi, _settingsAccessor)
                {
                    Id = companyFromDto.Id,
                    Name = companyFromDto.Name,
                    CreatedDate = companyFromDto.DateOfCreation.ToString()
                };
                companies.Add(company);
            }
            return companies;
        }
    }
}

