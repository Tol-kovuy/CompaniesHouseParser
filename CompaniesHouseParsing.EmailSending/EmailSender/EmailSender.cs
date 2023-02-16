using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;

namespace CompaniesHouseParsing.EmailSending
{
    public class EmailSender 
    {  

    /// var emailBeilse = new EmailMessageBuilder();
    /// var message = emailBeilse
    ///     .WithText("HEllo World")
    ///     .WithSubject("READ MOTHER FUCKER")
    ///     .Build()
    ///     ;
    /// var emailSmtpFactory = new EmailSmtpClientFactory();
    /// var emailSmtpClient = emailSmtpFactory.Create("smtp.google.com", 567, ...);
    /// emailSmtpClient.Send(message);

    // todo: use patern builder read and tell OM about it
    //public interface IEmailMessageBuilder
    //{
    //    IEmailMessageBuilder WithText(string text);
    //    IEmailMessageBuilder ToRcepient(string recepientEmailAddress);
    //    IEmailMessage Build();
    //}
    // example
    // IEmailMessageBuilder builder = ...
    // var message = builder
    //      .WithText("HEllo world")
    //      .Build();

    //public interface IEmailMessage
    //{
    //    string Text { get; }
    //}

    // TODO: proxy pattern?
    //public interface IEmailSmtpClient
    //{
    //    void Send(IEmailMessage message);
    //}

    //public interface IEmailSmtpClientFactory
    //{
    //    IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl);
    //}

    //public interface IEmailSmtpClient
    //{
    //    void Send(IEmailMessage message);
    //}

    // TODO: read about adapter pattern
    //public class EmailSmtpClient : IEmailSmtpClient
    //{
    //    private SmtpClient _client;
    //    public EmailSmtpClient(string host, int port, NetworkCredential credentials, bool enablessl)
    //    {
    //        _client = new SmtpClient(_emailForm.Smtp.Host)
    //        {
    //            Port = _emailForm.Smtp.Port,
    //            Credentials = new NetworkCredential(_emailForm.Smtp.Email, _emailForm.Smtp.Password),
    //            EnableSsl = true,
    //        };
    //    }

    //    public void Send(IEmailMessage message)
    //    {
    //        _client.Send(new MailMessage()
    //        {
    //            From = message.Text
    //        });
    //    }
    }
}
