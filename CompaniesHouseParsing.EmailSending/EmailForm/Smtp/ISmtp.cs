namespace CompaniesHouseParsing.EmailSending
{
    public interface ISmtp
    {
        string Email { get; }
        string Host { get; }
        string Password { get; }
        int Port { get; }
    }
}
