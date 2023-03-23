using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Api;

public interface IGetOfficerRequest : ITransientDependency
{
    string CompanyId { get; }
    string ApiToken { get; }
}