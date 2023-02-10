using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class LinkDto
    {
        [JsonProperty(PropertyName = "self")]
        public string Self { get; set; }
        [JsonProperty(PropertyName = "officer")]
        public AppointmentDto OfficerAppointments { get; set; }
    }
}