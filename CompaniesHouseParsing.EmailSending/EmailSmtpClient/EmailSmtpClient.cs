using System.Net;
using System.Net.Mail;

namespace CompaniesHouseParsing.EmailSending
{
    public class EmailSmtpClient : IEmailSmtpClient
    {
        private SmtpClient _client; 

        public EmailSmtpClient(string host, int port, NetworkCredential credentials, bool enablessl)
        {
            _client = new SmtpClient(host)
            {
                Port = port,
                Credentials = credentials,
                EnableSsl = enablessl,
            };
        }

        public void Send(IEmailMessage message)
        {
            var smtpMessage = new MailMessage(message.Sender, message.Recipient)
            {
                Subject = message.Subject,
                Body = message.Text
            };

            _client.Send(smtpMessage);
        }
    }
}
