using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompaniesListDto
    {
        [JsonProperty(PropertyName = "items")]
        public List<CompanyDto> Companies { get; set; }
    }
}
