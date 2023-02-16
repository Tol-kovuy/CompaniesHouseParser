namespace CompaniesHouseParsing.EmailSending
{
    public interface IEmailMessageBuilder
    {
        IEmailMessage Build();
        IEmailMessageBuilder WithText(string text);
        IEmailMessageBuilder ToRcepient(string recepientEmailAddress);
        IEmailMessageBuilder From(string sender);
        IEmailMessageBuilder WithSubject(string subject);
    }
}