using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainParser;

public interface IDomainCompanyEmailSender : ITransientDependency
{
    Task SendAsync(ICompany message);
}
