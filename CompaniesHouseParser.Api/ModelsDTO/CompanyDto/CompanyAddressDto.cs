using Newtonsoft.Json;

namespace CompaniesHouseParser.Api;

public class CompanyAddressDto
{
    [JsonProperty(PropertyName = "address_line_1")]
    public string FullAddress { get; set; }
    [JsonProperty(PropertyName = "locality")]
    public string City { get; set; }
    [JsonProperty(PropertyName = "postal_code")]
    public string PostalCode { get; set; }
    [JsonProperty(PropertyName = "country")]
    public string Country { get; set; }
    [JsonProperty(PropertyName = "sic_codes")]
    public int[] SicCodes { get; set; }
}