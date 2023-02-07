using Newtonsoft.Json;

namespace CompaniesHouseParser.Api
{
    public class CompanyDto
    {
        [JsonProperty(PropertyName = "company_numbe")]
        public string Id { get; set;}
        [JsonProperty(PropertyName = "company_name")]
        public string Name { get; set;}
    }
}
