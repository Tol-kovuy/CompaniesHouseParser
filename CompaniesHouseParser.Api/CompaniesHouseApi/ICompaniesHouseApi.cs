using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Api;

public interface ICompaniesHouseApi : ITransientDependency
{
    Task<IList<CompanyDto>> GetCompaniesAsync(IGetCompaniesRequest request);
    Task<IList<OfficerDto>> GetOfficers(IGetOfficerRequest request);
}