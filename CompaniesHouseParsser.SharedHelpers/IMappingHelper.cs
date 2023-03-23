using AutoMapper;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.SharedHelpers;

public interface IMappingHelper : ISingletonDependency
{
    IMapper GetMapper();
}