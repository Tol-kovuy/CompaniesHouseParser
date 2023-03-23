using AutoMapper;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Mapping;

public interface ICompanyMapperFactory : ISingletonDependency
{
    IMapper Get();
}