using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompaniesListModelDto
    {
        [JsonProperty(PropertyName = "items")]
        public IList<CompanyModelDto>? CompaniesDto { get; set; }
    }
}
