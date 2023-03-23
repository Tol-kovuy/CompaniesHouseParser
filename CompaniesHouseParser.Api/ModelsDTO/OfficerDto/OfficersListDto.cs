using Newtonsoft.Json;

namespace CompaniesHouseParser.Api;

public class OfficersListDto
{
    [JsonProperty(PropertyName = "items")]
    public List<OfficerDto> Officers { get; set; } 
    [JsonProperty(PropertyName = "total_results")]
    public int TotalResults { get; set; }
    [JsonProperty(PropertyName = "inactive_count")]
    public bool InactiveCoun { get; set; }
    [JsonProperty(PropertyName = "resigned_count")]
    public int ResignedCount { get; set; }
    [JsonProperty(PropertyName = "active_count")]
    public bool ActiveCount { get; set; }
    [JsonProperty(PropertyName = "start_index")]
    public int StartIndex { get; set; }
}
