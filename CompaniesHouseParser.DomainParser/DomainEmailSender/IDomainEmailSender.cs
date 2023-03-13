using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.DomainParser
{
    public interface IDomainEmailSender : ITransientDependency
    {
        void SendTextMessage(string message);
    }
}
