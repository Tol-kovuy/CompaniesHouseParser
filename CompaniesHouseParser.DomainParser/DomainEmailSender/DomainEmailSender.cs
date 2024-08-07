﻿using CompaniesHouseParser.Email;
using CompaniesHouseParser.Settings;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CompaniesHouseParser.DomainParser;

public class DomainEmailSender : IDomainEmailSender
{
    private readonly IApplicationSettings _applicationSettings;
    private readonly IEmailMessageBuilder _emailMessageBuilder;
    private readonly ILogger<EmailSmtpClient> _logger;

    public DomainEmailSender(
        IApplicationSettingsAccessor settingsAccessor,
        IEmailMessageBuilder emailMessageBuilder,
        ILogger<EmailSmtpClient> logger
        )
    {
        _applicationSettings = settingsAccessor.Get();
        _emailMessageBuilder = emailMessageBuilder;
        _logger = logger;
    }

    public void SendTextMessage(string message)
    {
        var emailMessage = BuildEmailMessage(message);
        Send(emailMessage);
    }
    private void Send(IEmailMessage message)
    {
        var emailSmtpClient = BuildSmtpClient();
        emailSmtpClient.Send(message);
    }

    private IEmailMessage BuildEmailMessage(string message)
    {
        var mailSettings = _applicationSettings.Email.EmailAddresses;
        var buildedMessage = _emailMessageBuilder
            .From(mailSettings.EmailAddressFrom)
            .ToRcepient(mailSettings.EmailAddressTo)
            .WithTextBody(message)
            .WithSubject(_applicationSettings.Subject)
            .Build();
        return buildedMessage;
    }

    private IEmailSmtpClient BuildSmtpClient()
    {
        var emailSmtpFactory = new EmailSmtpClientFactory(_logger);
        var smtpSettings = _applicationSettings.Smtp;
        var emailSmtpClient = emailSmtpFactory.Create(
            smtpSettings.Host,
            smtpSettings.Port,
            new NetworkCredential(smtpSettings.UserName, smtpSettings.Password),
            enablessl: true
        );
        return emailSmtpClient;
    }
}
