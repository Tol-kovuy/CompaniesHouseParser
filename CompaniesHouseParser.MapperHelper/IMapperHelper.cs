using CompaniesHouseParser.Api;
using CompaniesHouseParser.DomainApi;

namespace CompaniesHouseParser.MapperHelper
{
    public interface IMapperHelper
    {
        Company GetMappingCompany(CompanyDto companyDto);
        Officer GetMappingOfficer(OfficerDto officerDto);
    }
}