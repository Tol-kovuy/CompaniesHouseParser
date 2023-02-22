namespace CompaniesHouseParser.DomainApi
{
    public class DomainGetCompaniesRequest : IDomainGetCompaniesRequest
    {
        public DateTime IncorporatedFrom { get; set; }
    }
}
