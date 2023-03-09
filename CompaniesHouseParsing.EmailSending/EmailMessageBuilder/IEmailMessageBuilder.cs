namespace CompaniesHouseParser.Email
{
    public interface IEmailMessageBuilder
    {
        IEmailMessage Build();
        IEmailMessageBuilder WithTextBody(string text);
        IEmailMessageBuilder ToRcepient(string recepientEmailAddress);
        IEmailMessageBuilder From(string sender);
        IEmailMessageBuilder WithSubject(string subject);
    }
}