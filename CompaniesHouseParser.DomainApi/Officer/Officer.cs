using CompaniesHouseParser.DomainShared;

namespace CompaniesHouseParser.DomainApi
{
    public class Officer : IOfficer
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string Nationality { get; set; }
        public int YearOfBirthday { get; set; }
        public int MonthOfBirthday { get; set; }
        public string Position { get; set; }
        public DateTime AppointedOn { get; set; }
        public string CountryOfResidence { get; set; }
        public string Self { get; set; }
        public string OfficerAppointments { get; set; }
        public string AddresLine1 { get; set; }
        public string AddresLine2 { get; set; }
        public string Apartments { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public bool IsNationality(string nationality)
        {
            if (Nationality == null)
            {
                return false;
            }

            if (Nationality.Equals(nationality, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
