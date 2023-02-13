using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;

namespace CompaniesHouseParsing.EmailSending
{
    public class EmailSender : IEmailSender
    {
        private readonly IEmailForm _emailForm;

        public EmailSender(IEmailForm emailForm)
        {
            _emailForm = emailForm;
        }

        public void SendMessage()
        {
            var mailWithImg = GetMailWithImg();
            var smtpClient = CreateSmtpClient();
            smtpClient.Send(mailWithImg);
        }
        private MailMessage GetMailWithImg()
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            var htmlView = GetImageHtml(_emailForm.Content, "C:\\CH_for_mail.png");
            mail.AlternateViews.Add(htmlView);
            mail.From = new MailAddress(_emailForm.Sender);
            mail.To.Add(_emailForm.Recipient);
            mail.Subject = _emailForm.Subject;

            return mail;
        }

        private AlternateView GetImageHtml(string text, string nameImage)
        {
            var linkedImage = new LinkedResource("C:\\CH_for_mail.png");
            linkedImage.ContentType = new ContentType(MediaTypeNames.Image.Jpeg);

            var htmlView = AlternateView.CreateAlternateViewFromString(
                text, null, "text/html");
            htmlView.LinkedResources.Add(linkedImage);
            return htmlView;
        }

        private SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient(_emailForm.Smtp.Host)
            {
                Port = _emailForm.Smtp.Port,
                Credentials = new NetworkCredential(_emailForm.Smtp.Email, _emailForm.Smtp.Password),
                EnableSsl = true,
            };

            return smtpClient;
        }
    }

    // todo: use patern builder read and tell OM about it
    public interface IEmailMessageBuilder
    {
        IEmailMessageBuilder WithText(string text);
        IEmailMessage Build();
    }
    // example
    // IEmailMessageBuilder builder = ...
    // var message = builder
    //      .WithText("HEllo world")
    //      .Build();

    public interface IEmailMessage
    {
        string Text { get; }
    }

    // TODO: proxy pattern?
    public interface IEmailSmtpClient
    {
        void Send(IEmailMessage message);
    }

    public interface IEmailSmtpClientFactory
    {
        IEmailSmtpClient Create(string host, int port, NetworkCredential credentials, bool enablessl);
    }
}
