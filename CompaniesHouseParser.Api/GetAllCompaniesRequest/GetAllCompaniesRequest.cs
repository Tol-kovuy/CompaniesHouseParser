namespace CompaniesHouseParser.Api;

public class GetAllCompaniesRequest : IGetCompaniesRequest
{
    public DateTime IncorporatedFrom { get; set; }
    public int CompaniesCount { get; set; }
    public string ApiToken { get; set; }
    public DateTime SearchIncorporatedFrom { get; set; }
}
