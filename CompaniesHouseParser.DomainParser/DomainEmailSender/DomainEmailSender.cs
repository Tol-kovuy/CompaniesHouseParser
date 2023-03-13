using CompaniesHouseParser.Email;
using CompaniesHouseParser.Settings;
using NetCore.AutoRegisterDi;
using System.Net;

namespace CompaniesHouseParser.DomainParser;

public class DomainEmailSender : IDomainEmailSender
{
    private readonly IApplicationSettings _applicationSettings;
    private readonly IEmailMessageBuilder _emailMessageBuilder;

    public DomainEmailSender(
        IApplicationSettingsAccessor settingsAccessor,
        IEmailMessageBuilder emailMessageBuilder
        )
    {
        _applicationSettings = settingsAccessor.Get();
        _emailMessageBuilder = emailMessageBuilder;
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
            .WithSubject(_applicationSettings.Subject) // TODO: add subject to settings
            .Build();
        return buildedMessage;
    }

    private IEmailSmtpClient BuildSmtpClient()
    {
        var emailSmtpFactory = new EmailSmtpClientFactory();
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
