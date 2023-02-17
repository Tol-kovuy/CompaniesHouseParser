using System.Net;

namespace CompaniesHouseParser.Email
{
    public interface IEmailSmtpClientFactory
    {
        IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl);
    }
}