namespace CompaniesHouseParser.Api
{
    public interface IGetCompaniesRequest
    {
        int CompaniesCount { get; }
        DateTime IncorporatedFrom { get; }
        string ApiToken { get; }
    }
}