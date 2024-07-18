using Newtonsoft.Json;

namespace CompaniesHouseParser.Api;

public class OfficerDto
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "officer_role")]
    public string Role { get; set; }

    [JsonProperty(PropertyName = "nationality")]
    public string Nationality { get; set; }

    [JsonProperty(PropertyName = "date_of_birth")]
    public DateOfBirthOfficerDto? DateOfBirth { get; set; }

    [JsonProperty(PropertyName = "occupation")]
    public string Position { get; set; }

    [JsonProperty(PropertyName = "appointed_on")]
    public DateTime? AppointedOn { get; set; }

    [JsonProperty(PropertyName = "country_of_residence")]
    public string CountryOfResidence { get; set; }

    [JsonProperty(PropertyName = "address")]
    public AddressDto Address { get; set; }

    [JsonProperty(PropertyName = "links")]
    public LinkDto Links { get; set; }

    [JsonProperty(PropertyName = "resigned_on")]
    public string ResignedOn { get; set; }
}
