using AutoMapper;
using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.Mapping;

public class CompanyMapperFactory : ICompanyMapperFactory
{
    private readonly ICompaniesHouseApi _companiesHouseApi;
    private readonly IApplicationSettingsAccessor _applicationSettings;
    private readonly IMapper _mapper;
    private readonly MapperConfiguration _mapperConfiguration;

    public CompanyMapperFactory(
        IMapper mapper,
        ICompaniesHouseApi companiesHouseApi,
        IApplicationSettingsAccessor applicationSettings
        )

    {
        _mapper = mapper;
        _companiesHouseApi = companiesHouseApi;
        _applicationSettings = applicationSettings;

        _mapperConfiguration = new MapperConfiguration(config =>
        {
            config.CreateMap<CompanyDto, Company>()
                .ConstructUsing(x => new Company
                (
                    _mapper,
                    _companiesHouseApi, 
                    _applicationSettings
                 ))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateOfCreation))
                .ForMember(dest => dest.FullAddress, opt => opt.MapFrom(src => src.Address.FullAddress))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.SicCodes, opt => opt.MapFrom(src => src.Address.SicCodes));

            //config.CreateMap<OfficerDto, Officer>()
            //   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //   .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            //   .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
            //   .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
            //   .ForMember(dest => dest.MonthOfBirthday, opt => opt.MapFrom(src => src.DateOfBirth.Month))
            //   .ForMember(dest => dest.YearOfBirthday, opt => opt.MapFrom(src => src.DateOfBirth.Year))
            //   .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            //   .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
            //   .ForMember(dest => dest.AddresLine1, opt => opt.MapFrom(src => src.Address.AddresLine1))
            //   .ForMember(dest => dest.AddresLine2, opt => opt.MapFrom(src => src.Address.AddresLine2))
            //   .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
            //   .ForMember(dest => dest.Apartments, opt => opt.MapFrom(src => src.Address.Apartments))
            //   .ForMember(dest => dest.CountryOfResidence, opt => opt.MapFrom(src => src.CountryOfResidence))
            //   .ForMember(dest => dest.OfficerAppointments, opt => opt.MapFrom(src => src.Links.OfficerAppointments))
            //   .ForMember(dest => dest.Self, opt => opt.MapFrom(src => src.Links.Self))
            //   .ForMember(dest => dest.AppointedOn, opt => opt.MapFrom(src => src.AppointedOn));
        });
    }

    public IMapper Create()
    {
        return _mapperConfiguration.CreateMapper();
    }
}
