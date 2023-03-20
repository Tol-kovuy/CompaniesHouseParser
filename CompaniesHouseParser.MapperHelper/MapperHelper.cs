using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.Mapping;

namespace CompaniesHouseParser.MapperHelper
{
    public class MapperHelper : IMapperHelper
    {
        private ICompanyMapperFactory _companyMapperFactory;
        public MapperHelper(ICompanyMapperFactory companyMapperFactory)
        {
            _companyMapperFactory = companyMapperFactory;
        }

        public Company GetMappingCompany(CompanyDto companyDto)
        {
            var company = _companyMapperFactory.Create().Map<Company>(companyDto);
            return company;
        }

        public Officer GetMappingOfficer(OfficerDto officerDto)
        {
            var officer = _companyMapperFactory.Create().Map<Officer>(officerDto);
            return officer;
        }
    }
}