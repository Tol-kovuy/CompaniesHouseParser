﻿using AutoMapper;
using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Mapping
{
    public class MappingProfile : Profile, ITransientDependency
    {
        public MappingProfile()
        {
            CreateMap<CompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateOfCreation))
                .ForPath(dest => dest.Address.FullAddress, opt => opt.MapFrom(src => src.Address.FullAddress))
                .ForPath(dest => dest.Address.City, opt => opt.MapFrom(src => src.Address.City))
                .ForPath(dest => dest.Address.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForPath(dest => dest.Address.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForPath(dest => dest.Address.SicCodes, opt => opt.MapFrom(src => src.Address.SicCodes));
            CreateMap<OfficerDto, Officer>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Nationality, opt => opt.MapFrom(src => src.Nationality))
                .ForMember(dest => dest.MonthOfBirthday, opt => opt.MapFrom(src => src.DateOfBirth.Month))
                .ForMember(dest => dest.YearOfBirthday, opt => opt.MapFrom(src => src.DateOfBirth.Year))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Address.Country))
                .ForMember(dest => dest.AddresLine1, opt => opt.MapFrom(src => src.Address.AddresLine1))
                .ForMember(dest => dest.AddresLine2, opt => opt.MapFrom(src => src.Address.AddresLine2))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(dest => dest.Apartments, opt => opt.MapFrom(src => src.Address.Apartments))
                .ForMember(dest => dest.CountryOfResidence, opt => opt.MapFrom(src => src.CountryOfResidence))
                .ForMember(dest => dest.OfficerAppointments, opt => opt.MapFrom(src => src.Links.OfficerAppointments))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => src.Links.Self))
                .ForMember(dest => dest.AppointedOn, opt => opt.MapFrom(src => src.AppointedOn));
        }
    }
}