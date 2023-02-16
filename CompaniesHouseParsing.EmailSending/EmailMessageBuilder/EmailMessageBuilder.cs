using System.Net;

namespace CompaniesHouseParsing.EmailSending
{
    public class EmailMessageBuilder : IEmailMessageBuilder
    {
        public IEmailMessage Build()
        {
            return new EmailMessage()
            {
                Sender = "krotkrotowskij@gmail.com",
                Recipient = "smarty.maks13#gmail.com",
                Subject = "Test",
                Text = "Test text"
            };
        }

        public IEmailMessageBuilder WithText(string text)
        {
            var message = Build();
            var client = new EmailSmtpClientFactory();
            var createClient = client.Create("smtp.gmail.com", 587, new NetworkCredential { Password = "cwztcchhlltrzskg" }, true);
            var sendMess = createClient.Send(message);
        }

        public IEmailMessageBuilder ToRcepient(string recepientEmailAddress)
        {
            
        }
    }
}
