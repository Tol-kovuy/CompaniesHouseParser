using CompaniesHouseParser.Settings;

namespace CompaniesHouseParser.DomainParser
{
    public interface IParser
    {
        Task SendResult();
    }
}
