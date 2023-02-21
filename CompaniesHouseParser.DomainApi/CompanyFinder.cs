using CompaniesHouseParser.Api;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi
{
    public class CompanyFinder
    {
        private ICompaniesHouseApi _companiesHouseApi = new CompaniesHouseApi();
        private IApplicationSettingsAccessor _appSettingsAccessor = new ApplicationSettingsAccessor();
        private IGetCompaniesRequest _getCompaniesRequest;
        private ICompanyHouseParsingStateAccessor _companyHouseParsingStateAccessor = new CompanyHouseParsingStateAccessor();
        private ApplicationSettings _applicationSettings;

        public async Task<IList<ICompany>> GetNewlyCreatedAsync()
        {
            var companies = new List<ICompany>();
            
            _getCompaniesRequest = new GetAllCompaniesRequest
            {
                ApiToken = _appSettingsAccessor.Get().CompaniesHouseApi.Token,
                CompaniesCount = _appSettingsAccessor.Get().CompaniesHouseApi.SearchCompaniesPerRequest,
                IncorporatedFrom = _companyHouseParsingStateAccessor.Get().Companies.LastIncorporatedFrom
            };
            var companiesFromDto = await _companiesHouseApi.GetCompaniesAsync(_getCompaniesRequest);

            foreach (var companyFromDto in companiesFromDto)
            {
                var company = new Company(_companiesHouseApi, _appSettingsAccessor)
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



    //public interface IDomainGetCompaniesRequest
    //{
    //    int CompaniesCount { get; set; }
    //    DateTime IncorporatedFrom { get; set; }
    //}

    //public class DomainGetCompaniesRequest : IDomainGetCompaniesRequest
    //{
    //    public int CompaniesCount { get; set; }
    //    public DateTime IncorporatedFrom { get; set; }
    //}

    //public interface IDomainCompaniesApi
    //{
    //    Task<IList<ICompany>> GetCompanies(IDomainGetCompaniesRequest requestApi);
    //}


    //public class DomainCompaniesApi
    //{
    //    private ICompaniesHouseApi _companiesHouseApi;
    //    private IApplicationSettings _applicationSettings;

    //    public DomainCompaniesApi(
    //        ICompaniesHouseApi companiesHouseApi,
    //        IApplicationSettingsAccessor settingsAccessor
    //        )
    //    {
    //        _companiesHouseApi = companiesHouseApi;
    //        _applicationSettings = settingsAccessor.Get();
    //    }

    //    public async Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi)
    //    {
    //        var request = new GetAllCompaniesRequest
    //        {
    //            ApiToken = _applicationSettings.CompaniesHouseApi.Token,
    //            CompaniesCount = requestApi.CompaniesCount,
    //            IncorporatedFrom = requestApi.IncorporatedFrom
    //        };

    //        var companiesDtos = await _companiesHouseApi.GetCompaniesAsync(request);

    //        var companies = new List<ICompany>();
    //        foreach (var companyFromDto in companiesDtos)
    //        {
    //            var company = new Company(_companiesHouseApi, _applicationSettings)
    //            {
    //                Id = companyFromDto.Id,
    //                Name = companyFromDto.Name,
    //                CreatedDate = companyFromDto.DateOfCreation.ToString()
    //            };
    //            companies.Add(company);
    //        }
    //        return companies;
    //    }
    //}

}
