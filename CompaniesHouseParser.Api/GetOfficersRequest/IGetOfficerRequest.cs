namespace CompaniesHouseParser.Api
{
    public interface IGetOfficerRequest
    {
        string CompanyId { get; }
        string ApiToken { get; }
    }
}