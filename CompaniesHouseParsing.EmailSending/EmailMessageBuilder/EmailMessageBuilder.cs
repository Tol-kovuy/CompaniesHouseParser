using System.Net;

namespace CompaniesHouseParsing.EmailSending
{
    public class EmailMessageBuilder : IEmailMessageBuilder
    {
        private string _text;
        private string _subject;
        private string _to;
        private string _from;

        public IEmailMessage Build()
        {
            return new EmailMessage
            {
                Sender = _from,
                Recipient = _to,
                Subject = _subject,
                Text = _text
            };
        }
        
        public IEmailMessageBuilder WithText(string text)
        {
            _text = text;
            return this;
        }
        
        public IEmailMessageBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public IEmailMessageBuilder ToRcepient(string recepientEmailAddress)
        {
            _to = recepientEmailAddress;
            return this;
        }

        public IEmailMessageBuilder From(string sender)
        { 
            _from = sender; 
            return this;
        }
    }
}
