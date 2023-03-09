using CompaniesHouseParser.DomainShared;

namespace CompaniesHouseParser.DomainParser;

public interface IDomainCompanyEmailSender
{
    Task SendAsync(ICompany message);
}
