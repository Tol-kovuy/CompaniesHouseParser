namespace CompaniesHouseParsing.EmailSending
{
    public interface IEmailForm
    {
        string Sender { get; }
        string Recipient { get; }
        ISmtp Smtp { get; }
        string Subject { get; }
        string Content { get; }
    }
}
