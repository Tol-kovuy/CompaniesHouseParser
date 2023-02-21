namespace CompaniesHouseParser.DomainApi
{
    public interface IOfficer
    {
        string Name { get; }
        string Nationality { get; }
        string Role { get; }
        string Position { get; }
        string AddresLine1 { get; }
        string AddresLine2 { get; }
        string Apartments { get; }
        string City { get; }
        string Country { get; }
        string CountryOfResidence { get; }
        int MonthOfBirthday { get; }
        int YearOfBirthday { get; }
        DateTime AppointedOn { get; }
        string OfficerAppointments { get; }
        string PostalCode { get; }
        string Self { get; }
       
    }
}