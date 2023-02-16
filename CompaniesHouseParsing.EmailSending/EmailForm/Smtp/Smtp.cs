namespace CompaniesHouseParsing.EmailSending
{
    public class Smtp : ISmtp
    {
        public string Email { get; set; }
        public string Host { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
