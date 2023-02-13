using System.Net;
using System.Net.Mail;

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
            var smtpClient = CreateSmtpClient();
            try
            {
                smtpClient.Send(_emailForm.Sender, _emailForm.Recipient,
                    _emailForm.Subject, _emailForm.Content);
                Console.WriteLine("Email was sent...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"Message : {ex}");
                throw;
            }

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
}
