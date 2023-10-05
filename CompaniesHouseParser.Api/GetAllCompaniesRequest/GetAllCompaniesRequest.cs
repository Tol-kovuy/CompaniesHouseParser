namespace CompaniesHouseParser.Api;

public class GetAllCompaniesRequest : IGetCompaniesRequest
{
    public int CompaniesCount { get; set; }
    public string ApiToken { get; set; }
    public DateTime SearchIncorporatedFrom { get; set; }
}
