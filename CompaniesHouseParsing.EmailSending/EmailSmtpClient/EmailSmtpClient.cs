using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace CompaniesHouseParser.Email;

public class EmailSmtpClient : IEmailSmtpClient
{
    private SmtpClient _client;
    public ILogger Logger { get; set; }
    public string Host { get; }
    public int Port { get; }
    public NetworkCredential Credentials { get; }
    public bool Enablessl { get; }

    public EmailSmtpClient(string host, int port, NetworkCredential credentials, bool enablessl, ILogger<EmailSmtpClient> logger)
    {
        _client = new SmtpClient(host)
        {
            Port = port,
            Credentials = credentials,
            EnableSsl = enablessl,
        };
        Logger = logger;
    }

    public void Send(IEmailMessage message)
    {
        try
        {
            var smtpMessage = new MailMessage(message.Sender, message.Recipient)
            {
                Subject = message.Subject,
                Body = message.Text
            };

            _client.Send(smtpMessage);
        }
        catch (SmtpException ex)
        {
            if (ex.Message.Contains("Authentication Required"))
            {
                Logger.LogError("SMTP authentication failed. Check your credentials.");
            }
            else if (ex.Message.Contains("Secure connection required"))
            {
                Logger.LogError("SMTP server requires a secure connection.Use SSL / TLS.");
            }
            else
            {
                Logger.LogError($"SMTP error: {ex.Message}");
            }

            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError($"An error occurred: {ex.Message}");
            throw;
        }
    }
}
