using CompaniesHouseParser.Api;
using CompaniesHouseParser.Domain;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Mapping;
using CompaniesHouseParser.Settings;
using CompaniesHouseParser.Shared;

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
            SearchIncorporatedFrom = requestApi.IncorporatedFrom
        };

        IList<CompanyDto> activeCompaniesDtos;
        try
        {
            var companiesDtos = await _companiesHouseApi.GetCompaniesAsync(request);
            await WriteCompanyIdsToFile(companiesDtos);
            activeCompaniesDtos = GetActiveCompanies(companiesDtos);
        }
        catch (Exception)
        {

            throw;
        }
        
        var companies = new List<ICompany>();
        foreach (var companyFromDto in activeCompaniesDtos)
        {
            var company = _companyMapperFactory.Get().Map<Company>(companyFromDto);
            companies.Add(company);
        }

        var response = new DomainGetCompaniesResponse
        {
            Companies = companies,
            // for today date we will return COMPANIES but
            // SearchIncorporatedFrom <  DateTime.Now will return false
            // and CanFetchMoreCompanies will be false
            CanFetchMoreCompanies = request.SearchIncorporatedFrom.Date < DateTime.Now.Date
        };
        return response;
    }
    
    private async Task WriteCompanyIdsToFile(IList<CompanyDto> companies)
    {
        var directoryName = FilePaths.AllCompaniesDirectoryName;
        var fileName = FilePaths.AllCompaniesFileName;

        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }

        var filePath = Path.Combine(directoryName, fileName);

        using (var writer = new StreamWriter(filePath, append: true))
        {
            foreach (var companyDto in companies)
            {
                await writer.WriteLineAsync(companyDto.Id);
            }
        }
    }

    public async Task<ICompany> GetCompanyByIdAsync(string id)
    {
        var settings = _applicationSettings.Get().CompaniesHouseApi;

        var request = new GetOfficerRequest()
        {
            CompanyId = id,
            ApiToken = settings.Token
        };
        var companyDto  = await _companiesHouseApi.GetCompanyById(request);
        var company = _companyMapperFactory.Get().Map<Company>(companyDto);

        return company;
    }

    private IList<CompanyDto> GetActiveCompanies(IList<CompanyDto> allCompanies)
    {
        return allCompanies.Where(company => company.StatusIsActive).ToList(); 
    }
}

