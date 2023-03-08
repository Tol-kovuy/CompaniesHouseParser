using CompaniesHouseParser.Email;

namespace CompaniesHouseParser.DomainParser
{
    public interface IDomainEmailSender
    {
        IEmailMessage BuildEmailMessage(string message);
        void Send(IEmailMessage message);
    }
}
