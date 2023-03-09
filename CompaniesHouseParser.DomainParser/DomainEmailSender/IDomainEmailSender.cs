using CompaniesHouseParser.Email;

namespace CompaniesHouseParser.DomainParser
{
    public interface IDomainEmailSender
    {
        void SendTextMessage(string message);
    }
}
