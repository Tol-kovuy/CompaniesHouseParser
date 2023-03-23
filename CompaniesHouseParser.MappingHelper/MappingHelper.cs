using AutoMapper;
using CompaniesHouseParser.Mapping;
using CompaniesHouseParser.SharedHelpers;

namespace CompaniesHouseParser.MappingHelper;

public class MappingHelper : IMappingHelper
{

    private ICompanyMapperFactory _companyMapperFactory;
    public MappingHelper(ICompanyMapperFactory companyMapperFactory)
    {
        _companyMapperFactory = companyMapperFactory;
    }

    public IMapper GetMapper()
    {
        return _companyMapperFactory.Create(); ;
    }
}