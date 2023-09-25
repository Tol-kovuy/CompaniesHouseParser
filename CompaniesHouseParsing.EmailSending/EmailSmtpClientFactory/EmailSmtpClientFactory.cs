using Microsoft.Extensions.Logging;
using System.Net;

namespace CompaniesHouseParser.Email;

public class EmailSmtpClientFactory : IEmailSmtpClientFactory
{
    public ILogger<EmailSmtpClient> Logger { get; set; }
    public EmailSmtpClientFactory(ILogger<EmailSmtpClient> logger)
    {
        Logger = logger; 
    }
    public IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl)
    {
        return new EmailSmtpClient(host, port, credentials, enablessl, Logger);
    }
}
