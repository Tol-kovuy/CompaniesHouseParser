﻿using System.Net;

namespace CompaniesHouseParser.Email;

public class EmailSmtpClientFactory : IEmailSmtpClientFactory
{
    public IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl)
    {
        return new EmailSmtpClient(host, port, credentials, enablessl);
    }
}
