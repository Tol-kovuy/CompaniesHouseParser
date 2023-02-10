namespace CompaniesHouseParser.Api
{
    public class GetOfficerRequest : IGetOfficerRequest
    {
        public string CompanyId { get; set; }
        public string ApiToken { get; set; }
    }
}
