using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class DateOfBirthOfficerDto
    {
        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }
        [JsonProperty(PropertyName = "month")]
        public string Month { get; set; }
    }
}