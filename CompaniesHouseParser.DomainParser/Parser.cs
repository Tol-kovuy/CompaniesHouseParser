using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Email;
using CompaniesHouseParser.Settings;
using CompaniesHousseParser.DomainSearchFilter;
using System.Net;
using System.Text;

namespace CompaniesHouseParser.DomainParser
{
    // Use module Email + Settings
    public interface IDomainEmailSender
    {
        void Send(IEmailMessage message);
    }

    // Use module Email + IDomainEmailSender
    public interface IDomainCompanyEmailSender
    {
        void SendAsync(ICompany message);
    }

    public class Parser : IParser
    {
        private IDomainFilteredSearch _domainFilteredSearch;
        private IEmailMessageBuilder _emailMessageBuilder;
        private IDomainCompanyEmailSender _emailSender;

        public Parser(
            IDomainFilteredSearch domainFilteredSearch,
            IEmailMessageBuilder emailMessageBuilder,
            IApplicationSettingsAccessor applicationSettings
            )
        {
            _domainFilteredSearch = domainFilteredSearch;
            _emailMessageBuilder = emailMessageBuilder;
            _applicationSettings = applicationSettings;
        }

        public async Task ExecuteAsync()
        {
            var companies = await GetFilteredCompaniesAsync();
            foreach (var company in companies)
            {
                var emailBody = await CreateEmailBody(company);
                var emailMessage = BuildEmailMessage(emailBody);
                //var emailSmtpClient = BuildSmtpClient();
                //emailSmtpClient.SendAsync(emailMessage);
                _emailSender.Send(emailMessage);
            }
        }

        private IEmailSmtpClient BuildSmtpClient()
        {
            var emailSmtpFactory = new EmailSmtpClientFactory();
            // TODO: code duplicate
            var smtpSettings = _applicationSettings.Get().Smtp;
            var emailSmtpClient = emailSmtpFactory.Create(
                smtpSettings.Host, 
                smtpSettings.Port,
                new NetworkCredential
                {
                    UserName = smtpSettings.UserName,
                    Password = smtpSettings.Password
                },
                true);

            return emailSmtpClient;
        }
        private IEmailMessage BuildEmailMessage(string message) 
        {
            var mailSettings = _applicationSettings.Get().Email.EmailAddresses;
            var buildedMessage = _emailMessageBuilder
                .From(mailSettings.EmailAddressFrom)
                .ToRcepient(mailSettings.EmailAddressTo)
                .WithTextBody(message)
                // TODO: add subject to settings
                .WithSubject("Fucking Sabject") // create method GetSabject()?
                .Build();
            return buildedMessage;
        }
        private async Task<IList<ICompany>> GetFilteredCompaniesAsync()
        {
            return await _domainFilteredSearch.GetFilteredCompaniesAsync();
        }

        private async Task<string> CreateEmailBody(ICompany company)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Company ID: ")
                         .AppendLine(company.Id)
                         .Append("Name of the company: ")
                         .AppendLine(company.Name)
                         .Append("Created Date: ")
                         .AppendLine(company.CreatedDate.ToString());
            stringBuilder.AppendLine("List of all officers: ");

            foreach (var officers in await company.GetOfficersAsync())
            {
                stringBuilder.Append("Full Name: ")
                             .AppendLine(officers.Name)
                             .Append("Nationality: ")
                             .AppendLine(officers.Nationality)
                             .Append("Month Of Birthday: ")
                             .AppendLine(officers.MonthOfBirthday.ToString())
                             .Append("Year Of Birthday: ")
                             .AppendLine(officers.YearOfBirthday.ToString())
                             .Append("Role: ")
                             .AppendLine(officers.Role)
                             .Append("Position: ")
                             .AppendLine(officers.Position)
                             .Append("Country: ")
                             .AppendLine(officers.Country)
                             .Append("Country Of Residence: ")
                             .AppendLine(officers.CountryOfResidence)
                             .Append("City: ")
                             .AppendLine(officers.City)
                             .Append("Postal Code: ")
                             .AppendLine(officers.PostalCode)
                             .Append("Apartments: ")
                             .AppendLine(officers.Apartments)
                             .Append("AddresLine1: ")
                             .AppendLine(officers.AddresLine1)
                             .Append("AddresLine2: ")
                             .AppendLine(officers.AddresLine2)
                             .Append("Officer Appointments: ")
                             .AppendLine(officers.OfficerAppointments)
                             .Append("Self: ")
                             .AppendLine(officers.Self)
                             .Append("Appointed On: ")
                             .AppendLine(officers.AppointedOn.ToString());
            }
            var oneMessage = stringBuilder.ToString();
            return oneMessage;
        }
    }
}
