using AutoMapper;
using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainApi;

public class DomainCompaniesApi : IDomainCompaniesApi
{
    private ICompaniesHouseApi _companiesHouseApi;
    private IApplicationSettingsAccessor _applicationSettings;
    private readonly IMapper _mapper;

    public DomainCompaniesApi(
        IMapper mapper,
        ICompaniesHouseApi companiesHouseApi,
        IApplicationSettingsAccessor settingsAccessor
        )
    {
        _mapper = mapper;
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
            var company = _mapper.Map<CompanyDto, Company>(companyFromDto);

            //var company = new Company(_mapper, _companiesHouseApi, _applicationSettings)
            //{
            //    Id = companyFromDto.Id,
            //    Name = companyFromDto.Name,
            //    CreatedDate = companyFromDto.DateOfCreation
            //};
            companies.Add(company);
        }

        

        return companies;
    }
}

