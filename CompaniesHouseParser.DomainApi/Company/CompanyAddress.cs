using Newtonsoft.Json;

namespace CompaniesHouseParser.DomainApi
{
    public class CompanyAddress
    {
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int[] SicCodes { get; set; }
    }
}