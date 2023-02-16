using System.Net;
using System.Net.Mail;

namespace CompaniesHouseParsing.EmailSending
{
    // todo: read about patter factory
    public class EmailSmtpClientFactory : IEmailSmtpClientFactory
    {
        public IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl)
        {
            return new EmailSmtpClient(host, port, credentials, enablessl);
        }
    }
}
