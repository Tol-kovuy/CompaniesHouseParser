using Newtonsoft.Json;

namespace CompaniesHouseParser.Api;

public class CompanyDto
{
    [JsonProperty(PropertyName = "company_number")]
    public string Id { get; set;}

    [JsonProperty(PropertyName = "company_name")]
    public string Name { get; set;}

    [JsonProperty(PropertyName = "company_status")]
    public string Status { get; set; }

    [JsonProperty(PropertyName = "company_type")]
    public string Type { get; set; }

    [JsonProperty(PropertyName = "date_of_creation")]
    public DateTime DateOfCreation { get; set; }

    [JsonProperty(PropertyName = "registered_office_address")]
    public CompanyAddressDto Address { get; set; }

    [JsonIgnore]
    public bool StatusIsActive => Status == "active";
}
