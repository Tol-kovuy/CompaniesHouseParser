namespace CompaniesHouseParser.Email;

public class EmailMessage : IEmailMessage
{
    public string Text { get; set; }
    public string Subject { get; set; }
    public string Sender { get; set; }
    public string Recipient { get; set; }
}
