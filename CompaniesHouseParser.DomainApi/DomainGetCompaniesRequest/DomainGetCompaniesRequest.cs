namespace CompaniesHouseParser.DomainApi
{
    public class DomainGetCompaniesRequest : IDomainGetCompaniesRequest
    {
        public int CompaniesCount { get; set; }
        public DateTime IncorporatedFrom { get; set; }
    }
}
