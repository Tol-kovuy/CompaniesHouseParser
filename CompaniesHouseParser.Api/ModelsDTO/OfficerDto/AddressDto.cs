using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class AddressDto
    {
        [JsonProperty(PropertyName = "address_line_1")]
        public string AddresLine1 { get; set; }
        [JsonProperty(PropertyName = "address_line_2")]
        public string? AddresLine2 { get; set; }
        [JsonProperty(PropertyName = "premises")]
        public string Apartments { get; set; }
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "locality")]
        public string City { get; set; }
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }
    }
}