using NetCore.AutoRegisterDi;

namespace CompaniesHouseParser.Settings;

public class ResultMailingAddress : IResultMailingAddress
{
    public string EmailAddressFrom { get; set; }
    public string EmailAddressTo { get; set; }
}
