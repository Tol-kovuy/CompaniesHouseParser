using CompaniesHouseParser.Api;
using CompaniesHouseParser.Domain;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Mapping;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi;

public interface IDomainGetCompaniesResponse
{
    IList<ICompany> Companies { get; }
    bool CanFetchMoreCompanies { get; }
}

public class DomainGetCompaniesResponse : IDomainGetCompaniesResponse
{
    public IList<ICompany> Companies { get; set; }
    public bool CanFetchMoreCompanies { get; set; }
}

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

    public async Task<IDomainGetCompaniesResponse> GetCompaniesAsync(IDomainGetCompaniesRequest requestApi)
    {
        var settings = _applicationSettings.Get().CompaniesHouseApi;
        var request = new GetAllCompaniesRequest
        {
            ApiToken = settings.Token,
            CompaniesCount = settings.SearchCompaniesPerRequest,
            IncorporatedFrom = requestApi.IncorporatedFrom,
            SearchIncorporatedFrom = requestApi.IncorporatedFrom
        };

        IList<CompanyDto> companiesDtos;
        try
        {
            companiesDtos = await _companiesHouseApi.GetCompaniesAsync(request);
        }
        catch (Exception)
        {

            throw;
        }
        
        var companies = new List<ICompany>();
        foreach (var companyFromDto in companiesDtos)
        {
            var company = _companyMapperFactory.Get().Map<Company>(companyFromDto);
            companies.Add(company);
        }

        var response = new DomainGetCompaniesResponse
        {
            Companies = companies,
            CanFetchMoreCompanies = companies.Count == settings.SearchCompaniesPerRequest
        };
        return response;
    }
}

