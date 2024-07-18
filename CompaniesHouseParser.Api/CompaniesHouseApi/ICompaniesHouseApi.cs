using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Api;

public interface ICompaniesHouseApi : ITransientDependency
{
    Task<IList<CompanyDto>> GetCompaniesAsync(IGetCompaniesRequest request);
    Task<CompanyDto> GetCompanyById(IGetOfficerRequest requestApi);
    Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest request);
}