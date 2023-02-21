namespace CompaniesHouseParser.Api
{
    public interface IGetCompaniesRequest
    {
        int CompaniesCount { get; set; }
        DateTime IncorporatedFrom { get; set; }
        string ApiToken { get; set; }
    }
}