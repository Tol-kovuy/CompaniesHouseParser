using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Email
{
    public interface IEmailSmtpClient : ITransientDependency
    {
        void Send(IEmailMessage message);
    }
}