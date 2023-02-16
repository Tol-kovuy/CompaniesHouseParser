using System.Net;

namespace CompaniesHouseParsing.EmailSending
{
    public interface IEmailSmtpClientFactory
    {
        IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl);
    }
}