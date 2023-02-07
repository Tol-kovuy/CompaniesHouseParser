using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompaniesListDto
    {
        [JsonProperty(PropertyName = "items")]
        public IList<CompanyDto> CompaniesDto { get; set; }
    }
}
