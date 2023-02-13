namespace CompaniesHouseParsing.EmailSending
{
    public class EmailForm : IEmailForm
    {
        public string Sender { get; set; }

        public string Recipient { get; set; }

        public ISmtp Smtp { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }
    }
}
