using CompaniesHouseParser.IoC;
using System.Net;

namespace CompaniesHouseParser.Email;

public interface IEmailSmtpClientFactory : ITransientDependency
{
    IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl);
}