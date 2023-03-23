using CompaniesHouseParser.Api;
using CompaniesHouseParser.Domain;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Mapping;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi;

public class DomainCompaniesApi : IDomainCompaniesApi
{
    private ICompaniesHouseApi _companiesHouseApi;
    private IApplicationSettingsAccessor _applicationSettings;
    private ICompanyMapperFactory _companyMapperFactory;

    public DomainCompaniesApi(
        ICompanyMapperFactory companyMapperFactory,
        ICompaniesHouseApi companiesHouseApi,
        IApplicationSettingsAccessor settingsAccessor
        )
    {
        _companyMapperFactory = companyMapperFactory;
        _companiesHouseApi = companiesHouseApi;
        _applicationSettings = settingsAccessor;
    }

    public async Task<IList<ICompany>> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi)
    {
        var settings = _applicationSettings.Get().CompaniesHouseApi;
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
            var company = _companyMapperFactory.Get().Map<Company>(companyFromDto);
            companies.Add(company);
        }
        return companies;
    }
}

