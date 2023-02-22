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

        public async Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi)
        {
            var request = new GetAllCompaniesRequest
            {
                ApiToken = _settingsAccessor.Get().CompaniesHouseApi.Token,
                CompaniesCount = requestApi.CompaniesCount,
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

