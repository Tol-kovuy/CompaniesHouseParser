using System.Net.Mail;

namespace CompaniesHouseParsing.EmailSending
{
    public interface IEmailSender
    {
        void SendMessage();
    }
}