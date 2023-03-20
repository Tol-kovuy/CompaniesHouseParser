using AutoMapper;
using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Settings;
using CompaniesHouseParser.SharedHelpers;

namespace CompaniesHouseParser.DomainApi;

public class Company : ICompany
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string FullAddress { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public int SicCodes { get; set; }
    public DateTime CreatedDate { get; set; }

    private IList<IOfficer> _officers;
    private readonly ICompaniesHouseApi _companiesHouseApi;
    private readonly IApplicationSettings _applicationSettings;
    private readonly IMapper _mapper;

    public Company(
        IMapper mapper,
        ICompaniesHouseApi companiesHouseApi,
        IApplicationSettingsAccessor applicationSettingsAccessor
        )
    {
        _companiesHouseApi = companiesHouseApi;
        _applicationSettings = applicationSettingsAccessor.Get();
        _mapper = mapper;
    }

    public async Task<IList<IOfficer>> GetOfficersAsync()
    {
        if (_officers != null)
        {
            return _officers;
        }

        var officerRequest = new GetOfficerRequest()
        {
            ApiToken = _applicationSettings.CompaniesHouseApi.Token,
            CompanyId = Id
        };

        _officers = new List<IOfficer>();
        var officersFromDto = await _companiesHouseApi.GetOfficers(officerRequest);
        foreach (var officerFromDto in officersFromDto)
        {
            var officer = _mapper.Map<OfficerDto, Officer>(officerFromDto);
            _officers.Add(officer);
        }
        return _officers;
    }

    public async Task<bool> HasOfficerWithNationalityAsync(string nationality)
    {
        _officers = await GetOfficersAsync();
        foreach (var officer in _officers)
        {
            if (officer.IsNationality(nationality))
            {
                return true;
            }
        }
        return false;
    }
}

