using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class OfficerListDto
    {
        [JsonProperty(PropertyName = "items")]
        public IList<OfficerDto> Officers { get; set; }
    }
}
