using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi
{
    public class DomainCompaniesApi : IDomainCompaniesApi
    {
        private ICompaniesHouseApi _companiesHouseApi;
        private IApplicationSettings _applicationSettings;

        public DomainCompaniesApi(
            ICompaniesHouseApi companiesHouseApi,
            IApplicationSettingsAccessor settingsAccessor
            )
        {
            _companiesHouseApi = companiesHouseApi;
            _applicationSettings = settingsAccessor.Get();
        }

        public async Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi)
        {
            var settings = _applicationSettings.CompaniesHouseApi;
            var request = new GetAllCompaniesRequest
            {
                ApiToken = settings.Token,
                CompaniesCount = settings.SearchCompaniesPerRequest,
                IncorporatedFrom = requestApi.IncorporatedFrom
            };

            var companiesDtos = await _companiesHouseApi.GetCompaniesAsync(request);

            var companies = new List<ICompany>();
            foreach (var companyFromDto in companiesDtos)
            {
                var company = new Company(_companiesHouseApi, _applicationSettings)
                {
                    Id = companyFromDto.Id,
                    Name = companyFromDto.Name,
                    CreatedDate = companyFromDto.DateOfCreation
                };
                companies.Add(company);
            }
            return companies;
        }
    }
}

