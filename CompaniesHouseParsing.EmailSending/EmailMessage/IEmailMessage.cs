namespace CompaniesHouseParser.Email;

public interface IEmailMessage
{
    string Subject { get; }
    string Text { get; }
    string Sender { get; }
    string Recipient { get; }
}
