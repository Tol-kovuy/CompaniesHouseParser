using CompaniesHouseParser.Email;
using CompaniesHouseParser.Settings;
using System.Net;

namespace CompaniesHouseParser.DomainParser;

// Use module Email + Settings
public class DomainEmailSender : IDomainEmailSender
{
    private IApplicationSettings _applicationSettings;
    private IEmailMessageBuilder _emailMessageBuilder;


    public DomainEmailSender(
        IApplicationSettingsAccessor settingsAccessor,
        IEmailMessageBuilder emailMessageBuilder
        )
    {
        _applicationSettings = settingsAccessor.Get();
        _emailMessageBuilder = emailMessageBuilder;
    }

    public void Send(IEmailMessage message)
    {
        var emailSmtpClient = BuildSmtpClient();
        emailSmtpClient.Send(message);
    }

    public IEmailMessage BuildEmailMessage(string message)
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
        var emailSmtpClient = emailSmtpFactory.Create
            (
                smtpSettings.Host,
                smtpSettings.Port,
                new NetworkCredential
                {
                    UserName = smtpSettings.UserName,
                    Password = smtpSettings.Password,
                },
                true
            );
        return emailSmtpClient;
    }
}
