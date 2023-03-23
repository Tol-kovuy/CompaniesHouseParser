using Newtonsoft.Json;

namespace CompaniesHouseParser.Api;

public class AppointmentDto
{
    [JsonProperty(PropertyName = "appointments")]
    public string Appointments { get; set; }
}