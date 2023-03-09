using CompaniesHouseParser.DomainApi;
using CompaniesHouseParser.DomainShared;
using CompaniesHouseParser.Email;
using System.Text;

namespace CompaniesHouseParser.DomainParser;

// Use module Email + IDomainEmailSender
public class DomainCompanyEmailSender : IDomainCompanyEmailSender
{
    private IDomainEmailSender _domainEmailSender;

    public DomainCompanyEmailSender(IDomainEmailSender domainEmailSender)
    {
        _domainEmailSender = domainEmailSender;
    }

    public async Task SendAsync(ICompany message)
    {
        var mailBody = await CreateEmailBodyAsync(message);
        //var emailMesasge = _domainEmailSender.BuildEmailMessage(mailBody);
        _domainEmailSender.Send(emailMesasge);
    }
   
    private async Task<string> CreateEmailBodyAsync(ICompany company)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder
            .Append("Company ID: ")
            .AppendLine(company.Id)
            .Append("Name of the company: ")
            .AppendLine(company.Name)
            .Append("Created Date: ")
            .AppendLine(company.CreatedDate.ToString())
            .AppendLine();

        stringBuilder.AppendLine("-----List of all officers-----");

        foreach (var officers in await company.GetOfficersAsync())
        {
            stringBuilder
                .AppendLine()
                .Append("Full Name: ")
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
                .AppendLine(officers.AppointedOn.ToString())
                .Append(new string('-', 50));
        }

        var stringMessage = stringBuilder.ToString();
        return stringMessage;
    }
}
