using CompaniesHouseParser.IoC;

namespace CompaniesHouseParser.Email;

public interface IEmailMessage : ITransientDependency
{
    string Subject { get; }
    string Text { get; }
    string Sender { get; }
    string Recipient { get; }
}
