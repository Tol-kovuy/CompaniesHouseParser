using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompanyModelDto
    {
        [JsonProperty(PropertyName = "company_number")]
        public string? CompanyId { get; set; }

        [JsonProperty(PropertyName = "company_name")]
        public string? CompanyName { get; set; }

        [JsonProperty(PropertyName = "date_of_creation")]
        public string? DateOfCreation { get; set; }

        [JsonProperty(PropertyName = "sic_codes")]
        public IList<string>? SIСCodes { get; set; }
    }
}
