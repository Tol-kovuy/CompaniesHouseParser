using AutoMapper;
using CompaniesHouseParser.Api;
using CompaniesHouseParser.Domain;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.Mapping;

public class CompanyMapperFactory : ICompanyMapperFactory
{
    private readonly ICompaniesHouseApi _companiesHouseApi;
    private readonly IApplicationSettingsAccessor _applicationSettings;
    private IMapper _mapper;
    private MapperConfiguration _mapperConfiguration;
    private IMapper _companyMapper;

    public CompanyMapperFactory(
        IMapper mapper,
        ICompaniesHouseApi companiesHouseApi,
        IApplicationSettingsAccessor applicationSettings
        )
    {
        _mapper = mapper;
        _companiesHouseApi = companiesHouseApi;
        _applicationSettings = applicationSettings;
        _mapperConfiguration = CompanyMap();
    }

    public IMapper Get()
    {
        if (_companyMapper != null)
        {
            return _companyMapper;
        }
        _companyMapper = CompanyMap().CreateMapper();
        return _companyMapper;
    }

    private MapperConfiguration CompanyMap()
    {
        _mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<CompanyDto, Company>()
                .ConstructUsing(x => new Company
                (
                    this._mapper,
                    this._companiesHouseApi,
                    this._applicationSettings
                 ))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateOfCreation))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Address.FullAddress))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.SicCodes, opt => opt.MapFrom(src => src.Address.SicCodes));
        });
        return _mapperConfiguration;
    }
}
