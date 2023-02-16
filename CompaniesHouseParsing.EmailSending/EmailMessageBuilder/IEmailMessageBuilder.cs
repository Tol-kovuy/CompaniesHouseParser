namespace CompaniesHouseParsing.EmailSending
{
    public interface IEmailMessageBuilder
    {
        IEmailMessage Build();
        IEmailMessageBuilder WithText(string text);
        IEmailMessageBuilder ToRcepient(string recepientEmailAddress);
    }
}