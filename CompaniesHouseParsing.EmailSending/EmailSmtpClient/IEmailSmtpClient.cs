namespace CompaniesHouseParsing.EmailSending
{
    public interface IEmailSmtpClient
    {
        void Send(IEmailMessage message);
    }
}