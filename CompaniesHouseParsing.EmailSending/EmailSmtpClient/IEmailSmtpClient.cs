namespace CompaniesHouseParser.Email
{
    public interface IEmailSmtpClient
    {
        void Send(IEmailMessage message);
    }
}